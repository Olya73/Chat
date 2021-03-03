export type ModalState = {
  modalType: MODAL_COMPONENTS | null,
  modalProps: object,
}

export interface Item {
  id: number,
  name: string,
  description: string,
  category: number,
}

export interface ItemToAdd {
  name: string,
  description: string,
  category: number,
}

export interface Category {
  id: number,
  text: string,
  description: string,
}

export interface CategoryToAdd {
  text: string,
  description: string,
}

export interface ErrorModalState {
	inputNameError: string,
	inputDescriptionError: string,
}

export interface ItemModalState {
  inputName: string,
  inputDescription: string,
  selectCategory: string,
  categoryId: number | null,
  errors: ErrorModalState,
}

export interface ItemModalProps {
	id?: number,
	name?: string, 
	description?: string,
	categoryId?: number, 
	action: string,
	title: string
}

export interface CategoryModalState {
  inputText: string,
  inputDescription: string,
  errors: ErrorModalState,
}

export interface ModalProps {
	title: string,
	onCancel: () => void,
	onSubmit: () => void,
}	

export interface DeleteModalProps extends ModalProps {
  id: number,
  message: string, 
  name: string, 
  action: MODAL_ACTIONS.deleteCategory | MODAL_ACTIONS.deleteItem, 
}

export interface AddItemModalProps extends ModalProps {
  id: number,
  name: string,
  description: string,
  categoryId: number,
  action: MODAL_ACTIONS.addItem | MODAL_ACTIONS.updateItem,
}

export interface AddCategoryModalProps extends ModalProps {
  id: number,
  text: string,
  description: string,
  action: MODAL_ACTIONS.addCategory | MODAL_ACTIONS.updateCategory,
}

export type ModalTypes = DeleteModalProps | AddItemModalProps | AddCategoryModalProps

export enum MODAL_COMPONENTS {
  DeleteModal = 'DELETE_MODAL',
  AddItemModal = 'ADD_ITEM_MODAL',
  AddCategoryModal ='ADD_CATEGORY_MODAL',
}

export enum MODAL_ACTIONS {
  addItem = 'ADD_ITEM',
	addCategory = 'ADD_CATEGORY',
		
	deleteItem = 'DELETE_ITEM',
	deleteCategory = 'DELETE_CATEGORY',

	updateItem = 'CHANGE_ITEM',
	updateCategory = 'CHANGE_CATEGORY',
}

