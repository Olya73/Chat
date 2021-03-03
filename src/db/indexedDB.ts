import { openDB, deleteDB, wrap, unwrap } from 'idb';
import { Switch } from 'react-router-dom';

const DATABASE_NAME = 'todo_list';
const DATABASE_VERSION = 1;

export const itemCategoryIndex = 'categorie_idx';
export const categoriesStore = 'Categories';
export const itemsStore = 'Items'

const db = openDB(DATABASE_NAME, DATABASE_VERSION, {
    upgrade(db1, oldVersion, newVersion, transaction)  {
      switch (oldVersion) {
        case 0:
          db1.createObjectStore(categoriesStore, {
            keyPath: 'id',
            autoIncrement: true,
          });
          db1.createObjectStore(itemsStore, {
            keyPath: 'id',
            autoIncrement: true,
          });
          const tx = transaction.objectStore(itemsStore)
          tx.createIndex(itemCategoryIndex, 'category')  
        case 1:
                    
      }
    }
});

export default db;