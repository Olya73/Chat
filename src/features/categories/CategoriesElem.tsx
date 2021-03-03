import React from 'react'
import { showModal } from '../modals/modalsSlice'
import { selectCategoryById } from './categoriesSlice'
import { useAppDispatch, useAppSelector } from '../../hooks'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { Item, Category, MODAL_COMPONENTS as modal, MODAL_ACTIONS as action } from '../../types'
import {
  faTrash,
  faPen
} from '@fortawesome/free-solid-svg-icons';



const CategoriesElem: React.FC<{ todo:number }> = ({todo}) => {
  const category = useAppSelector(state => selectCategoryById(state, todo))
  const { text, description } = category as Category;

  const dispatch = useAppDispatch(); 

  const handleClickDeleteCategory = () => {
    dispatch(showModal(modal.DeleteModal, {
      id: todo,
      title:'Удаление категории',
      message:'Вы уверены, что хотите удалить категорию',
      name: text,
      action: action.deleteCategory 
    }))
  }

  const handleClickChangeCategory = () => {
    dispatch(showModal(modal.AddCategoryModal, {
      title:'Редактирование категории',
      id: todo,
      text: text,
      description: description,
      action: action.addCategory,
    }))
  }

  return (
    <li className="list-elem">
      <div className="elem-info">
        <div><p className="element-name">{text}</p></div>
        <div><p className="element-description">{description}</p></div>
      </div>
      <div className="list-elem-buttons">
        <button onClick={handleClickChangeCategory} className="change-button button-icon icon"><FontAwesomeIcon icon={faPen} size="3x"/></button>
        <button onClick={handleClickDeleteCategory} className="delete-button button-icon icon"><FontAwesomeIcon icon={faTrash} size="3x"/></button>
      </div>
    </li>
  )
}

export default CategoriesElem