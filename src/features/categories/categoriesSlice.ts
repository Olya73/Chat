import { createSlice, createEntityAdapter, createAsyncThunk } from '@reduxjs/toolkit'
import { categoryService } from '../../db/CategorieService'
import { RootState } from '../../store'
import { Category } from '../../types'

const categoryAdapter = createEntityAdapter();

const initialState = categoryAdapter.getInitialState();

export const saveNewCategory = createAsyncThunk('category/saveNewCategory', async (category: Category) => {
  const categorytoadd = {
    text: category.text,
    description: category.description
  }
  const id = await categoryService.put(categorytoadd) as number;
  return await categoryService.get(id);
})

export const deleteCategory = createAsyncThunk('category/deleteCategory', async (id: number) => {
  await categoryService.delete(id);
  return id as number;
})

export const updateCategory = createAsyncThunk('category/updateCategory', async (category: Category) => {
  const { id } = category;
  await categoryService.put(category);
  return {id: id, changes: category}
})

export const getCategories = createAsyncThunk('category/getCategories', async () => {
  return await categoryService.getAll();
})

const categoriesSlice = createSlice({
  name: 'categories',
  initialState,
  reducers: {
  },
  extraReducers: builder => {
    builder
      .addCase(saveNewCategory.fulfilled, categoryAdapter.addOne)
      .addCase(saveNewCategory.rejected, (state, action) => {
        console.log('Error occured on an attempt to save new category to the global state', action.error)
      })
      .addCase(deleteCategory.fulfilled, categoryAdapter.removeOne)
      .addCase(deleteCategory.rejected, (state, action) => {
        console.log('Error occured on an attempt to remove category to the global state', action.error)
      })
      .addCase(updateCategory.fulfilled, categoryAdapter.updateOne)
      .addCase(updateCategory.rejected, (state, action) => {
        console.log('Error occured on an attempt to update category to the global state', action.error)
      })
      .addCase(getCategories.fulfilled, categoryAdapter.setAll)
  }
})

export const selectCategoryById = (state: RootState, id: number) => {
  return state.categories.entities[id]
}

export const selectCategories = (state: RootState) => state.categories.ids

export default categoriesSlice.reducer