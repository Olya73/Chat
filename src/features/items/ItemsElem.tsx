import React, { useState }  from 'react'
import { useSelector, useDispatch } from 'react-redux'
import { showModal } from '../modals/modalsSlice'
import { selectTodoById } from './itemsSlice'
import { selectCategoryById } from '../categories/categoriesSlice'
import { useAppDispatch, useAppSelector } from '../../hooks'
import { Item, Category, MODAL_COMPONENTS as modal, MODAL_ACTIONS as action } from '../../types'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import {
  faTrash,
  faPen,
  faFolder
} from '@fortawesome/free-solid-svg-icons';


const ItemsElem: React.FC<{ todo: number }> = ({ todo }) => {

  const dispatch = useAppDispatch(); 
  const item = useAppSelector(state => selectTodoById(state, todo))
 
  const { id, name, description, category: categoryId } = item as Item

  const category = useAppSelector(state => selectCategoryById(state, categoryId)) as Category;

  const handleClickDeleteItem = ():void => {
    dispatch(showModal(modal.DeleteModal, {
      id: id,
      title:'Удаление задачи',
      message:'Вы уверены, что хотите удалить задачу',
      name: name,
      action: action.deleteItem, 
    }))
  }
  const handleClickChangeItem = ():void => {
    dispatch(showModal(modal.AddItemModal, {
      title:'Редактирование задачи',
      id: id,
      name: name,
      description: description,
      categoryId: categoryId,
      action: action.updateItem,
    }))
  }

  return (
    <>
    <li className="list-elem">
      <div className="elem-info">
        <div className="name-category">
          <p className="element-name">{name}</p>
          { category && <p className="items-category-text"><span className="span-icon icon">
            <FontAwesomeIcon icon={faFolder} size="3x"/></span>{category.text}</p> }
        </div>
        <p className="element-description">{description}</p>       
      </div>
      <div className="list-elem-buttons">
        <button onClick={handleClickChangeItem} className="change-button button-icon icon">
          <FontAwesomeIcon icon={faPen} size="3x"/></button>
        <button onClick={handleClickDeleteItem} className="delete-button button-icon icon">
          <FontAwesomeIcon icon={faTrash} size="3x"/></button>         
      </div>
    </li>    
    </>
  )
}

export default ItemsElem