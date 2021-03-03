import React, { useState } from 'react'
import Modal from '../../components/Modal'
import { useStore } from 'react-redux'
import { hideModal, DataSubmit } from './modalsSlice'
import { labelErrorStyle, inputErrorStyle } from './styles'
import { useAppDispatch, useAppSelector } from '../../hooks'
import { ItemModalState, Category } from '../../types'
import { selectCategoryById } from '../categories/categoriesSlice'
import { ModalProps, AddItemModalProps } from '../../types'
import { INPUT_ITEM_DESCRIPTION, INPUT_ITEM_NAME } from './validationConsts'

const AddItemModal: React.FC<AddItemModalProps> = (props) => {
	const { id, name, description, categoryId, action, title } = props;

	const categories = useStore().getState().categories.entities as Array<Category>;
	
	const category = useAppSelector(state => selectCategoryById(state, categoryId)) as Category;
	console.log(categoryId)
	const [state, setState] = useState<ItemModalState>({
		inputName: name ? name : '',
		inputDescription: description ? description : '',
		selectCategory: category ? category.text : '',
		categoryId: categoryId ? categoryId : null,
		errors: { inputNameError: '', inputDescriptionError: ''}
	});
	
	const dispatch = useAppDispatch();

	const handleNameChange = ({ target: { value } }: React.ChangeEvent<HTMLInputElement>) => {				
		let errors = {}
		if (value.length >= INPUT_ITEM_NAME)
			errors = {
				inputNameError: `Ошибка! Длинна не должна превышать ${INPUT_ITEM_NAME}` 
			};			
		else
			errors = {inputNameError: null}
		setState({...state, inputName: value, errors: {...state.errors, ...errors}})
	}

	const handleDescriptionChange = ({ target: { value } }: React.ChangeEvent<HTMLTextAreaElement>): void => {
		let errors = {}
		if (value.length >= INPUT_ITEM_DESCRIPTION) 
			errors = {
				inputDescriptionError: `Ошибка! Длинна не должна превышать ${INPUT_ITEM_DESCRIPTION}` 
			};			
		else 
			errors = {inputDescriptionError: null}
		setState({...state, inputDescription: value, errors: {...state.errors, ...errors}})
	}

	const handleCategoryChange = ({ target: { options, value } }: React.ChangeEvent<HTMLSelectElement>): void => {
		const key = options.selectedIndex;
		const id = options[key].getAttribute('data-key') as string;
		var id_num: number | null = id ? +id : null;
		setState({...state, selectCategory: value, categoryId: id_num})
	}

	const handleClickCancel = () => {dispatch(hideModal())}

	const handleClickSubmit = () => {
		const {inputName: name, inputDescription: description, categoryId: category} = state;
		const data = {
			id: id,
			name,
			description,
			category
		}
		if (state.inputName.length === 0) {
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
		onSubmit: handleClickSubmit
	}	

	return (
		<Modal modal={modal}>
			<div className="name-category">
				<label>
					<span className="input-label req" style={state.errors.inputNameError ? labelErrorStyle : undefined}>Имя</span>
					<input 
						type="text" 
						placeholder="Введите имя задачи" 
						value={state.inputName} 
						onChange={handleNameChange} 
						style={state.errors.inputNameError ? inputErrorStyle : undefined}
					/>
					<span className="input-error">{state.errors.inputNameError}</span>
				</label>
				

				<label>
					<span className="input-label">Категория</span>
					<select value={state.selectCategory} onChange={handleCategoryChange}>
						<option value="" disabled selected>{'Выберите категорию'}</option>
						{Object.entries(categories).map(( [ key, value ] ) => (                    
								<option key={value.id} data-key={value.id} value={value.text}>{value.text}</option>
						))}
						<option data-key={null} value={'value'}>{''}</option>
					</select>
				</label>
			</div>
	
			<label>
				<span className="input-label" style={state.errors.inputDescriptionError ? labelErrorStyle : undefined}>Описание</span>
				<textarea 
					style={state.errors.inputDescriptionError ? inputErrorStyle : undefined} 
					placeholder="Введите описание задачи" 
					value={state.inputDescription} 
					onChange={handleDescriptionChange}
				/>
				<span className="input-error">{state.errors.inputDescriptionError}</span>
			</label>
				
		</Modal>
	)
}

export default AddItemModal;