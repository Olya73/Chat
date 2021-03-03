import { createSlice, createEntityAdapter, createAsyncThunk } from '@reduxjs/toolkit'
import { itemService } from '../../db/ItemService'
import { RootState } from '../../store'
import { Item } from '../../types'

const itemsAdapter = createEntityAdapter();

const initialState = itemsAdapter.getInitialState();

export const saveNewItem = createAsyncThunk('items/saveNewItem', async (item: Item) => {
  const itemtoadd = {
    name: item.name,
    description: item.description,
    category: item.category
  }
  const id = await itemService.put(itemtoadd) as number;
  return await itemService.get(id);
})

export const deleteItem = createAsyncThunk('items/deleteItem', async (id: number) => {
  await itemService.delete(id);
  return id;
})

export const updateItem = createAsyncThunk('items/updateItem', async (item: Item) => {
  const { id } = item;
  await itemService.put(item);
  return {id: id, changes: item }
})

export const getItems = createAsyncThunk('items/getItems', async () => {
  return await itemService.getAll(); 
})

const itemsSlice = createSlice({
  name: 'items',
  initialState,
  reducers: {
  },
  extraReducers: builder => {
    builder
      .addCase(saveNewItem.fulfilled, itemsAdapter.addOne)
      .addCase(saveNewItem.rejected, (state, action) => {
        console.log('Error occured on an attempt to save new item to the global state', action.error)
      })
      .addCase(deleteItem.fulfilled, itemsAdapter.removeOne)
      .addCase(deleteItem.rejected, (state, action) => {
        console.log('Error occured on an attempt to save new item to the global state', action.error)
      })
      .addCase(updateItem.fulfilled, itemsAdapter.updateOne)
      .addCase(updateItem.rejected, (state, action) => {
        console.log('Error occured on an attempt to save new item to the global state', action.error)
      })
      .addCase(getItems.fulfilled, itemsAdapter.setAll)
  }
})

export const selectTodoById = (state: RootState, id: number) => {
  return state.items.entities[id]
}

export const selectTodos = (state: RootState) => state.items.ids

export default itemsSlice.reducer