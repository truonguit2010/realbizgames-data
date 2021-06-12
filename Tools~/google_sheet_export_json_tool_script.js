// Includes functions for exporting active sheet or all sheets as JSON object (also Python object syntax compatible).
// Tweak the makePrettyJSON_ function to customize what kind of JSON to export.
 
var EXPORT_FOLDER = 'Constant';

var FORMAT_ONELINE   = 'One-line';
var FORMAT_MULTILINE = 'Multi-line';
var FORMAT_PRETTY    = 'Pretty';
 
var LANGUAGE_JS      = 'JavaScript';
var LANGUAGE_PYTHON  = 'Python';
 
var STRUCTURE_LIST = 'List';
var STRUCTURE_HASH = 'Hash (keyed by "id" column)';
 
/* Defaults for this particular spreadsheet, change as desired */
var DEFAULT_FORMAT = FORMAT_ONELINE;
var DEFAULT_LANGUAGE = LANGUAGE_JS;
var DEFAULT_STRUCTURE = STRUCTURE_HASH;
 
function onOpen() {
  var ss = SpreadsheetApp.getActiveSpreadsheet();
  var menuEntries = [
    {name: "Export JSON for all sheets", functionName: "exportAllSheets"},
    {name: "Export this sheet only", functionName: "exportSheet"},
  ];
  ss.addMenu("Export JSON", menuEntries);
} 
    
function exportOptions() {
  var doc = SpreadsheetApp.getActiveSpreadsheet();
  var app = UiApp.createApplication().setTitle('Export JSON');
  
  var grid = app.createGrid(4, 2);
  grid.setWidget(0, 0, makeLabel(app, 'Language:'));
  grid.setWidget(0, 1, makeListBox(app, 'language', [LANGUAGE_JS, LANGUAGE_PYTHON]));
  grid.setWidget(1, 0, makeLabel(app, 'Format:'));
  grid.setWidget(1, 1, makeListBox(app, 'format', [FORMAT_PRETTY, FORMAT_MULTILINE, FORMAT_ONELINE]));
  grid.setWidget(2, 0, makeLabel(app, 'Structure:'));
  grid.setWidget(2, 1, makeListBox(app, 'structure', [STRUCTURE_LIST, STRUCTURE_HASH]));
  grid.setWidget(3, 0, makeButton(app, grid, 'Export Active Sheet', 'exportSheet'));
  grid.setWidget(3, 1, makeButton(app, grid, 'Export All Sheets', 'exportAllSheets'));
  app.add(grid);
  
  doc.show(app);
}
 
function makeLabel(app, text, id) {
  var lb = app.createLabel(text);
  if (id) lb.setId(id);
  return lb;
}
 
function makeListBox(app, name, items) {
 
  var listBox = app.createListBox().setId(name).setName(name);
  listBox.setVisibleItemCount(1);
  
  var cache = CacheService.getPublicCache();
  var selectedValue = cache.get(name);
  Logger.log(selectedValue);
  for (var i = 0; i < items.length; i++) {
    listBox.addItem(items[i]);
    if (items[1] == selectedValue) {
      listBox.setSelectedIndex(i);
    }
  }
  return listBox;
}
 
function makeButton(app, parent, name, callback) {
  var button = app.createButton(name);
  app.add(button);
  var handler = app.createServerClickHandler(callback).addCallbackElement(parent);;
  button.addClickHandler(handler);
  return button;
}
 
function makeTextBox(app, name) { 
  var textArea    = app.createTextArea().setWidth('100%').setHeight('200px').setId(name).setName(name);
  return textArea;
}
 
function checkCanExportSheetName(sheetName)
{
  //return sheetName.length > 3;
  
  if (sheetName.length < 3)
  {
    return TRUE;
  }
  else
  {
    var bool1 = sheetName[0] == 'n';
    var bool2 = sheetName[1] == 'e';
    var bool3 = sheetName[2] == '_';

    return !(bool1 && bool2 && bool3);
  }
}

function exportSheet(e) {
  var ss = SpreadsheetApp.getActiveSpreadsheet();
  var folderName = ss.getName();
  var sheet = ss.getActiveSheet();
  var rowsData = getRowsData_(sheet, getExportOptions(e));
  var json = makeJSON_(rowsData, getExportOptions(e));
  
  var folders = DriveApp.getFoldersByName("JSONS");
  
  var folder;
  
  if (folders.hasNext())
  {
    folder = folders.next();
  }
  else
  {
    folder = DriveApp.createFolder("JSONS");
  }
  
  var folders = folder.getFoldersByName(folderName);
  
  if (folders.hasNext())
  {
    folder = folders.next();
  }
  else
  {
    folder = folder.createFolder(folderName);
  }
  
  var fileName = ss.getSheetName();
  fileName = fileName + ".json";
  
  files = folder.getFilesByName(fileName);
  
  if (files.hasNext())
  {
    file = files.next();
    file.setContent(json);
  }
  else
  {
    folder.createFile(fileName, json);
  }
  
  return displayText_(json);
}

function exportAllSheets(e) {
  
  var ss = SpreadsheetApp.getActiveSpreadsheet();
  var folderName = ss.getName();
  var sheets = ss.getSheets();
  var sheetsData = {};
  
  var folders = DriveApp.getFoldersByName("JSONS");
  
  var folder;
  
  if (folders.hasNext())
  {
    folder = folders.next();
  }
  else
  {
    folder = DriveApp.createFolder("JSONS");
  }
  
  var folders = folder.getFoldersByName(folderName);
  
  if (folders.hasNext())
  {
    folder = folders.next();
  }
  else
  {
    folder = folder.createFolder(folderName);
  }
  
  for (var i = 0; i < sheets.length; i++) {
    var sheet = sheets[i];
    var fileName = sheet.getSheetName();
    if (checkCanExportSheetName(fileName))
    {
      var rowsData = getRowsData_(sheet, getExportOptions(e));
      var json = makeJSON_(rowsData, getExportOptions(e));
      
      fileName = fileName + ".json";
      
      files = folder.getFilesByName(fileName);
      
      if (files.hasNext())
      {
        file = files.next();
        file.setContent(json);
      }
      else
      {
        folder.createFile(fileName, json);
      }
    }
  }
  return displayText_("Exported Completed");
}
 
  
function getExportOptions(e) {
  var options = {};
  
  options.language = e && e.parameter.language || DEFAULT_LANGUAGE;
  options.format   = e && e.parameter.format || DEFAULT_FORMAT;
  options.structure = e && e.parameter.structure || DEFAULT_STRUCTURE;
  
  var cache = CacheService.getPublicCache();
  cache.put('language', options.language);
  cache.put('format',   options.format);
  cache.put('structure',   options.structure);
  
  Logger.log(options);
  return options;
}
 
function makeJSON_(object, options) {
  var jsonString;
  if (options.format == FORMAT_PRETTY) {
    jsonString = JSON.stringify(object, null, 4);
  } 
  else {
     jsonString = JSON.stringify(object, null);//Utilities.jsonStringify(object);
  }
  
  if (options.language == LANGUAGE_PYTHON) {
    // add unicode markers
    jsonString = jsonString.replace(/"([a-zA-Z]*)":\s+"/gi, '"$1": u"');
  }
  
    //var find;  
    //find = String.fromCharCode(34) + '{';
    //jsonString = jsonString.replaceAll( find, '{');
    //find = '}' + String.fromCharCode(34);
    //jsonString = jsonString.replaceAll( find, '}');
    //find = String.fromCharCode(34) + '[';
    //jsonString = jsonString.replaceAll( find, '[');
    //find = ']' + String.fromCharCode(34);
    //jsonString = jsonString.replaceAll( find, ']');
    //find = String.fromCharCode(92);
    //jsonString = jsonString.replaceAll( find, '');
    
    //jsonString = jsonString.replaceAll( 'empty', '');
  
  return jsonString;
}

String.prototype.replaceAll = function (find, replace) {
    var str = this;
    return str.replace(new RegExp(find.replace(/[-\/^$*+?.()|{}]/g, '\\$&'), 'g'), replace);
};

function displayText_(text) {
  var html = HtmlService.createHtmlOutput(text);
  SpreadsheetApp.getUi() // Or DocumentApp or SlidesApp or FormApp.
      .showModalDialog(html, 'Dialog title');
  return html;
  
  //var app = UiApp.createApplication().setTitle('Exported JSON');
  //app.add(makeTextBox(app, 'json'));
  //app.getElementById('json').setText(text);
  //var ss = SpreadsheetApp.getActiveSpreadsheet(); 
  //ss.show(app);
  //return app; 
}
 
// getRowsData iterates row by row in the input range and returns an array of objects.
// Each object contains all the data for a given row, indexed by its normalized column name.
// Arguments:
//   - sheet: the sheet object that contains the data to be processed
//   - range: the exact range of cells where the data is stored
//   - columnHeadersRowIndex: specifies the row number where the column names are stored.
//       This argument is optional and it defaults to the row immediately above range; 
// Returns an Array of objects.
function getRowsData_(sheet, options) {
 
  var maxCol = sheet.getMaxColumns();
  var headersRange = sheet.getRange(1, 1, sheet.getFrozenRows(), sheet.getMaxColumns());
  var headers = headersRange.getValues()[0];
  
  for (var a = 0; a < headers.length; ++a)
  {
    var key = normalizeHeader_(headers[a]);
    
    if (key.length == 0)
    {
      maxCol = a;
      break;
    }
  }
  
  headersRange = sheet.getRange(1,1, sheet.getFrozenRows(), maxCol)
  headers = headersRange.getValues()[0];
  
  var dataRange = sheet.getRange(sheet.getFrozenRows()+1, 1, sheet.getMaxRows(), maxCol);
  var objects = getObjects_(dataRange.getValues(), normalizeHeaders_(headers));
  if (options.structure == STRUCTURE_HASH) {
    var objectsById = {};
    objects.forEach(function(object) {
      objectsById[object.id] = object;
      
    });
    return objectsById;
  } else {
    return objects;
  }
}
 
// getColumnsData iterates column by column in the input range and returns an array of objects.
// Each object contains all the data for a given column, indexed by its normalized row name.
// Arguments:
//   - sheet: the sheet object that contains the data to be processed
//   - range: the exact range of cells where the data is stored
//   - rowHeadersColumnIndex: specifies the column number where the row names are stored.
//       This argument is optional and it defaults to the column immediately left of the range; 
// Returns an Array of objects.
function getColumnsData_(sheet, range, rowHeadersColumnIndex) {
  rowHeadersColumnIndex = rowHeadersColumnIndex || range.getColumnIndex() - 1;
  var headersTmp = sheet.getRange(range.getRow(), rowHeadersColumnIndex, range.getNumRows(), 1).getValues();
  var headers = normalizeHeaders_(arrayTranspose_(headersTmp)[0]);
  return getObjects(arrayTranspose_(range.getValues()), headers);
}
 
 
// For every row of data in data, generates an object that contains the data. Names of
// object fields are defined in keys.
// Arguments:
//   - data: JavaScript 2d array
//   - keys: Array of Strings that define the property names for the objects to create
function getObjects_(data, keys) {
  var objects = [];
  for (var i = 0; i < data.length; ++i) {
    var object = {};
    var hasData = false;
    for (var j = 0; j < data[i].length; ++j) {
      var cellData = data[i][j];
      if (isCellEmpty_(cellData)) {
        continue;
      }
      // TruongPS code
      var key = keys[j];
      var objkeys = key.split(".");
      if (objkeys.length > 1) {
        var deepKey = objkeys[0];
        if (object[deepKey] === undefined) {
          object[deepKey] = {};
        }
        var deepObj = object[deepKey];
        for (var kk = 1; kk < objkeys.length; kk++) {
          deepKey = objkeys[kk];

          if (deepObj[deepKey] === undefined) {
            if (kk == objkeys.length - 1) {
              deepObj[deepKey] = cellData;
            } else {
              deepObj[deepKey] = {};
              deepObj = deepObj[deepKey];
            }
          }
        }
      } else {
        object[keys[j]] = cellData;
      }
      

      // End TruongPS code
      // object[keys[j]] = cellData;
      hasData = true;
    }
    if (hasData) {
      objects.push(object);
    }
  }
  return objects;
}
 
// Returns an Array of normalized Strings.
// Arguments:
//   - headers: Array of Strings to normalize
function normalizeHeaders_(headers) {
  var keys = [];
  for (var i = 0; i < headers.length; ++i) {
    
    var key = normalizeHeader_(headers[i]);
  
    if (key.length > 0) {
      keys.push(key);
    }
  }
  return keys;
}
 
// Normalizes a string, by removing all alphanumeric characters and using mixed case
// to separate words. The output will always start with a lower case letter.
// This function is designed to produce JavaScript object property names.
// Arguments:
//   - header: string to normalize
// Examples:
//   "First Name" -> "firstName"
//   "Market Cap (millions) -> "marketCapMillions
//   "1 number at the beginning is ignored" -> "numberAtTheBeginningIsIgnored"
function normalizeHeader_(header) {
  var key = "";
  var upperCase = false;
  for (var i = 0; i < header.length; ++i) {
    var letter = header[i];
    if (letter == " " && key.length > 0) {
      key += "_";
      //upperCase = true;
      continue;
    }
    key += letter;
    // if (!isAlnum_(letter)) {
    //   continue;
    // }
    // if (key.length == 0 && isDigit_(letter)) {
    //   key += letter;
    // } else {
    //   key += letter;
    // }
    // if (upperCase) {
    //   upperCase = false;
    //   key += letter.toUpperCase();
    // } else {
    //   key += letter.toLowerCase();
    // }
  }
  return key;
}
 
// Returns true if the cell where cellData was read from is empty.
// Arguments:
//   - cellData: string
function isCellEmpty_(cellData) {
  return typeof(cellData) == "string" && cellData == "";
}
 
// Returns true if the character char is alphabetical, false otherwise.
function isAlnum_(char) {
  return char >= 'A' && char <= 'Z' ||
    char >= 'a' && char <= 'z' ||
    isDigit_(char);
}
 
// Returns true if the character char is a digit, false otherwise.
function isDigit_(char) {
  return char >= '0' && char <= '9';
}
 
// Given a JavaScript 2d Array, this function returns the transposed table.
// Arguments:
//   - data: JavaScript 2d Array
// Returns a JavaScript 2d Array
// Example: arrayTranspose([[1,2,3],[4,5,6]]) returns [[1,4],[2,5],[3,6]].
function arrayTranspose_(data) {
  if (data.length == 0 || data[0].length == 0) {
    return null;
  }
 
  var ret = [];
  for (var i = 0; i < data[0].length; ++i) {
    ret.push([]);
  }
 
  for (var i = 0; i < data.length; ++i) {
    for (var j = 0; j < data[i].length; ++j) {
      ret[j][i] = data[i][j];
    }
  }
 
  return ret;
}

function CREATELIST(value)
{
  //var find = char(124);
  
  var str = value.toString();
  
  if (str == "")
  {
    return str;
  }
  
  str = str.replaceAll('+', ',' );
  str = "[" + str + "]";
  
  return str;
}