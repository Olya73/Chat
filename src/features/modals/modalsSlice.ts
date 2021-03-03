import { createSlice, Middleware, PayloadAction } from '@reduxjs/toolkit'
import { ThunkAction, ThunkDispatch } from 'redux-thunk';
import { saveNewItem, deleteItem, updateItem } from '../items/itemsSlice'
import { saveNewCategory, deleteCategory, updateCategory } from '../categories/categoriesSlice'
import { ModalState, ModalProps, MODAL_COMPONENTS } from '../../types'
import { AppDispatch } from '../../store'
import { Action } from 'redux';
import { RootState } from '../../store'


const MODAL_ACTIONS: any = {
	'ADD_ITEM': saveNewItem,
	'ADD_CATEGORY': saveNewCategory,
		
	'DELETE_ITEM': deleteItem,
	'DELETE_CATEGORY': deleteCategory,

	'CHANGE_ITEM': updateItem,
	'CHANGE_CATEGORY': updateCategory,
}

const initialState: ModalState = {
	modalType: null,
	modalProps: {}
}

const modalsSlice = createSlice({
	name: 'modals',
	initialState,
	reducers: {
		showModal:{
			reducer(state, action: PayloadAction<ModalState>) {
				const { modalType, modalProps } = action.payload
				state.modalType = modalType;
				state.modalProps = modalProps;
			},
			prepare(modalType: MODAL_COMPONENTS, modalProps: object) {
				return {
					payload: { modalType, modalProps }
				}
			}
		},
		hideModal() {
			return initialState;            
		}
	}
})

export const middle: Middleware = storeAPI => next => action => {
  if (typeof action === 'function') {
		return action(storeAPI.dispatch, storeAPI.getState)
	}
	return next(action)     
}

export const DataSubmit = (action: string, data: object | number): ThunkAction<void, RootState, undefined, Action> => (dispatch: AppDispatch) => {
    if (MODAL_ACTIONS[action])
		{
			dispatch(MODAL_ACTIONS[action](data));
			dispatch(hideModal())
		}    	
}

export const { showModal, hideModal } = modalsSlice.actions
export { MODAL_ACTIONS };
export default modalsSlice.reducer;