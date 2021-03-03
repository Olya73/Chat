import React, { useState } from 'react'
import DeleteModal from './DeleteModal'
import AddItemModal from './AddItemModal'
import AddCategoryModal from './AddCategorieModal'
import { connect } from 'react-redux'
import { RootState } from '../../store'
import { ModalState } from '../../types'


const modalComponents = {
	'DELETE_MODAL': DeleteModal,
	'ADD_ITEM_MODAL': AddItemModal,
	'ADD_CATEGORY_MODAL': AddCategoryModal,
}

const ModalRoot:React.FC<ModalState> = ({ modalType, modalProps }) => {
	if (!modalType) { 
		return null;
	}
 
	const SpecificModal: any = modalComponents[modalType]
	return <SpecificModal {...modalProps}/>
}

export default connect(
    (state:RootState) => state.modals
)(ModalRoot)