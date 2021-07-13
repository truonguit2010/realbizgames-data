# Realbiz Games Data
  
In Realbiz Games, we divide data into 2 types as I listed below:
1. Master Data
2. User Playing Data
  
***Master Data:*** is the data that we define to balance/define the game for users. We usually use GoogleSheet to design these data. Google Sheet has many advantage to share and work together.  
***User Playing Data:*** is the data that is generated when the user playing the game. It can be user score, user selected theme, user level progress, etc.
  
As I have talked above, I can summarized:
1. Master Data: It is only read by the Application.
2. User Playing Data: It can be read and write by the Application.


## I. Master Data
  
![Design Data with GoogleSheet](Tools~/Sample.png)

***Step 1:*** Design master data. We use Google Sheet to design this type of data. Google Sheet is a good choice for sharing and work together. It is easy to integrate with our backend to run some productivity tasks.  
***Step 2:*** Export MasterData into json file that easy to work with development system. You can use [my-script](Tools~/google_sheet_export_json_tool_script.js) to do this job.  
***Step 3:*** Use this .json file as you want.  
  
![Design Data with GoogleSheet](Tools~/Result.png)
  
Here is an [example](https://docs.google.com/spreadsheets/d/1UW6Xrvq8k1lkGMBbbfvy-Ej1XrUgG6QfEHn_7ZZOIB0/edit?usp=sharing) for you.
  
#### How does our game read the MasterData?
  
The master data is read by the Unity Application follow the below diagram.
  
![Read Master Data](Images~/RealBizGames_Analysis-_MasterData.png)

#### Where can you store your master data
1. Inside Build (Local)
2. [Firebase Remote Config](https://firebase.google.com/products/remote-config)
3. [Firebase Cloud Storage](https://firebase.google.com/docs/storage)
4. [Unity Remote Config](https://unity.com/remote-config)
5. [AWS S3](https://aws.amazon.com/s3/)
6. Your Own Backend Service

## II. User Playing Data

![Read Write User Playing Data](Images~/RealBizGames_Analysis-UserPlayingData.png)

## III. Using this package

Insert the below line into your manifest.json

```
"com.realbizgames.data": "https://github.com/truonguit2010/realbizgames-data.git#v1.0.0",
```
# Reference
1. [C# Json Library - Newtonsoft.Json](https://github.com/jilleJr/Newtonsoft.Json-for-Unity)
  
  

