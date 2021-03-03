import db, {itemsStore} from './indexedDB'
import { ItemToAdd } from '../types'

class ItemService {

  async get(key: number) {
    try {
      const adb = await db;
      return await adb.transaction(itemsStore, 'readonly').objectStore(itemsStore).get(key);
    } catch (error) {
      console.log('failed to retrieve value on ', key);
      throw error;
    }
  }

  async getAll() {
    try {
      const adb = await db;
      return await adb.transaction(itemsStore, 'readonly').objectStore(itemsStore).getAll();
    } catch (error) {
      console.log('failed getall', error);
      throw error;
    }
  }

  async put(object: ItemToAdd) {
    try {
      console.log(object, 'put')
      const adb = await db;
      return await adb.transaction(itemsStore, 'readwrite').objectStore(itemsStore).put(object);
    } catch (error) {
      console.log('failed to add ', object);
      throw error;
    }
  }

  async delete(key: number) {
    try {
      const adb = await db;
      return await adb.transaction(itemsStore, 'readwrite').objectStore(itemsStore).delete(key);
    } catch (error) {
      console.log('failed to delete on ', key);
      throw error;
    }
  }

  async deleteAll() {
    try {
      const adb = await db;
      return await adb.transaction(itemsStore, 'readwrite').objectStore(itemsStore).clear();
    } catch (error) {
      console.log('failed to delete all ');
      throw error;
    }
  }
}

export  const itemService = new ItemService()