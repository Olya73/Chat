import { configureStore } from '@reduxjs/toolkit'

import itemsSlice from './features/items/itemsSlice'
import categoriesSlice from './features/categories/categoriesSlice'
import modalsSlice, {middle} from './features/modals/modalsSlice'

const store = configureStore({
  reducer: {
    modals: modalsSlice,
    items: itemsSlice,
    categories: categoriesSlice,
  },
  middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(middle),
})

export default store
export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch