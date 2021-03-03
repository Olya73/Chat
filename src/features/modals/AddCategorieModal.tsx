import React, { useState } from 'react'
import Modal from '../../components/Modal';
import { hideModal, DataSubmit } from './modalsSlice'
import { useAppDispatch } from '../../hooks'
import { CategoryModalState } from '../../types'
import { ModalProps, AddCategoryModalProps } from '../../types'
import { labelErrorStyle, inputErrorStyle } from './styles'
import { INPUT_CATEGORY_DESCRIPTION, INPUT_CATEGORY_NAME } from './validationConsts'

const AddCategoryModal: React.FC<AddCategoryModalProps> = (props) => {
	const { id, text, description, action, title } = props;

	const [state, setState] = useState<CategoryModalState>({
		inputText: text ? text : '',
		inputDescription: description ? description : '',
		errors: { inputNameError: '', inputDescriptionError: ''}
	}); 

	const handleTextChange = ({ target: { value } }: React.ChangeEvent<HTMLInputElement>): void => {        
		let errors = {}
		if (value.length >= INPUT_CATEGORY_NAME)
			errors = {
				inputNameError: `Ошибка! Длинна не должна превышать ${INPUT_CATEGORY_NAME}`
			};			
		else
			errors = {inputNameError: null}
		setState({...state, inputText: value, errors: {...state.errors, ...errors}})				
	}
	const handleDescriptionChange = ({ target: { value } }: React.ChangeEvent<HTMLTextAreaElement>): void => {
		let errors = {}
		if (value.length >= INPUT_CATEGORY_DESCRIPTION) 
			errors = {
				inputDescriptionError: `Ошибка! Длинна не должна превышать ${INPUT_CATEGORY_DESCRIPTION}` 
			};			
		else 
			errors = {inputDescriptionError: null}
		setState({...state, inputDescription: value, errors: {...state.errors, ...errors}})
	}

	const dispatch = useAppDispatch();

  const handleClickCancel = ():void => {dispatch(hideModal())}

	const handleSubmitClick = ():void => {
		const {inputText: text, inputDescription: description } = state;
		const data = {
				id: id,
				text,
				description,
		}
		if (state.inputText.length === 0) {
			setState({...state, errors: { ...state.errors, inputNameError: 'Ошибка! Поле обязательное' }})
			return;
		}
		if (state.errors.inputNameError || state.errors.inputDescriptionError)
		{
			alert('Неверно заполнены поля ввода');
			return;
		}	
		dispatch(DataSubmit(action,data))        			
	}

	const modal: ModalProps = {
		title,
		onCancel: handleClickCancel,
		onSubmit: handleSubmitClick
	}

	return (
		<Modal modal={modal}>
			<label>
				<span className="input-label req" style={state.errors.inputNameError ? labelErrorStyle : undefined}>Имя</span>
				<input 
					style={state.errors.inputNameError ? inputErrorStyle : undefined} 
					type="text" 
					placeholder="Введите имя задачи" 
					value={state.inputText} 
					onChange={handleTextChange}
				/>
				<span className="input-error">{state.errors.inputNameError}</span>
			</label>           

			<label>
				<span className="input-label" style={state.errors.inputDescriptionError ? labelErrorStyle : undefined}>Описание</span>
				<textarea 
					style={state.errors.inputDescriptionError ? inputErrorStyle : undefined} 
					placeholder="Введите описание задачи" value={state.inputDescription} 
					onChange={handleDescriptionChange}
				/>
				<span className="input-error">{state.errors.inputDescriptionError}</span>
			</label>
				
		</Modal>
	)
}

export default AddCategoryModal;