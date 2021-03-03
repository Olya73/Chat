import Modal from '../../components/Modal';
import React from 'react'
import { useDispatch, useSelector, useStore } from 'react-redux'
import { showModal, hideModal, DataSubmit } from './modalsSlice'
import { useAppDispatch, useAppSelector } from '../../hooks'
import { ModalProps, DeleteModalProps } from '../../types'

const DeleteModal:React.FC<DeleteModalProps> = (props) => {  
	const { id, message, name, action, title } = props;
	
	console.log(props)
	const dispatch = useAppDispatch();

	const handleClickCancel = ():void => {dispatch(hideModal())}

	const handleClickSubmit = ():void => {
		dispatch(DataSubmit(action, id))
	}		
	const modal: ModalProps = {
		title,
		onCancel: handleClickCancel,
		onSubmit: handleClickSubmit
	}
	return (
		<Modal modal={modal}>
				<p className="confirm-message">{`${message} "${name}"?`}</p>
		</Modal>
	)
}

export default DeleteModal;