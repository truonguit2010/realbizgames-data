using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RealbizGames.Data
{
    public class GenericDefaultMasterDataMediationRepository<T> : IMasterDataRepository<T>
    {

        private string localFilePath;
        private string firebaseKey;
        private IMasterDataMediator<T> masterDataMediator;
        private bool _isInit = false;

        public GenericDefaultMasterDataMediationRepository(string localFilePath, string firebaseKey)
        {
            this.localFilePath = localFilePath;
            this.firebaseKey = firebaseKey;

            this.masterDataMediator = new DefaultMasterDataMediator<T>(localFilePath, firebaseKey);
        }

        public List<T> FindAll()
        {
            lazyInit();
            return masterDataMediator.FindAll();
        }

        public List<T> FindAllByIds(List<string> ids)
        {
            lazyInit();
            return masterDataMediator.FindAllByIds(ids: ids);
        }

        public T FindById(string id)
        {
            lazyInit();
            return masterDataMediator.FindById(id: id);
        }

        public List<string> GetAllKey() {
            lazyInit();
            return masterDataMediator.GetAllKey();
        }

        public void init()
        {
            lazyInit();
        }

        public void lazyInit()
        {
            if (!_isInit) {
                _isInit = true;
                this.masterDataMediator.Load();
            }
            
        }

        public void refresh()
        {
            this.masterDataMediator.Reload();
        }
    }
}