import db, {categoriesStore, itemsStore, itemCategoryIndex } from './indexedDB'
import { CategoryToAdd } from '../types'
 
class CategoryService {

  async get(key: number) {
    try {
      const adb = await db;
      return await adb.transaction(categoriesStore, 'readonly').objectStore(categoriesStore).get(key);
    } catch (error) {
      console.log('failed to retrieve value on ', key);
      throw error;
    }
  }

  async getAll() {
    try {
      const adb = await db;
      return await adb.transaction(categoriesStore, 'readonly').objectStore(categoriesStore).getAll();
    } catch (error) {
      console.log('failed getall');
      throw error;
    }
  }

  async put(object: CategoryToAdd) {
    try {
      const adb = await db;
      return await adb.transaction(categoriesStore, 'readwrite').objectStore(categoriesStore).put(object);
    } catch (error) {
      console.log('failed to add ', object);
      throw error;
    }
  }

  async delete(key: number) {
    try {
      const adb = await db;
      const trans = adb.transaction([categoriesStore, itemsStore], 'readwrite');
      await trans.objectStore(categoriesStore).delete(key);
      let items = trans.objectStore(itemsStore);
      let request = await items.index(itemCategoryIndex).getAll(key);

      request.forEach((obj) => items.put({ ...obj, categorie: null }));
    } catch (error) {
      console.log('failed to delete on ', key, error);
      throw error;
    }
  }

  async deleteAll() {
    try {
      const adb = await db;
      return await adb.transaction(categoriesStore, 'readwrite').objectStore(categoriesStore).clear();
    } catch (error) {
      console.log('failed to delete all ');
      throw error;
    }
  }
}

export const categoryService = new CategoryService()