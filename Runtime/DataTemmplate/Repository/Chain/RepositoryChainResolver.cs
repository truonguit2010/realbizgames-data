using System.Collections.Generic;

namespace RealbizGames.Data
{
    public class RepositoryChainResolver<T> : IMasterDataRepository<T>
    {
        private List<IMasterDataRepository<T>> repositories;

        public RepositoryChainResolver(List<IMasterDataRepository<T>> repositories)
        {
            this.repositories = repositories;
        }

        public List<T> FindAll()
        {
            List<T> l = null;
            for (int i = 0; i < repositories.Count; i++)
            {
                l = repositories[i].FindAll();
                if (l != null && l.Count > 0)
                {
                    return l;
                }
            }
            return new List<T>();
        }

        public List<T> FindAllByIds(List<string> ids)
        {
            List<T> l = null;
            for (int i = 0; i < repositories.Count; i++)
            {
                l = repositories[i].FindAllByIds(ids);
                if (l != null && l.Count > 0)
                {
                    return l;
                }
            }
            return new List<T>();
        }

        public T FindById(string id)
        {
            T l = default;
            for (int i = 0; i < repositories.Count; i++)
            {
                l = repositories[i].FindById(id);
                if (l != null)
                {
                    return l;
                }
            }
            return default;
        }

        public void init()
        {
            for (int i = 0; i < repositories.Count; i++)
            {
                repositories[i].init();
            }
        }

        public void lazyInit()
        {
            for (int i = 0; i < repositories.Count; i++)
            {
                repositories[i].lazyInit();
            }
        }

        public void refresh()
        {
            for (int i = 0; i < repositories.Count; i++)
            {
                repositories[i].refresh();
            }
        }
    }
}