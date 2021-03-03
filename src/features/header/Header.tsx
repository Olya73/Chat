import React from 'react'
import { useDispatch } from 'react-redux'
import { showModal } from '../modals/modalsSlice'
import { NavLink } from 'react-router-dom';
import { MODAL_COMPONENTS as modal, MODAL_ACTIONS as action } from '../../types'

const Header:React.FC<{ currentList: string}> = ({ currentList }) => {
  
  const dispatch = useDispatch();
  const handleClickAdd = () => {
    if (currentList === 'Items')
      dispatch(showModal(modal.AddItemModal, {
        title:'Создание задачи',
        action: action.addItem
      }))
    else
      dispatch(showModal(modal.AddCategoryModal, {
        title:'Создание категории',
        action: action.addCategory
        }))
  };

  return (
    <header className="header">
      <nav>
        <ul className="nav-elems">
          <li className="home-link flex-grow-elem">
            <NavLink to="/">ToDo List</NavLink>
          </li>
          <li className="flex-grow-elem">
            <NavLink to='/tasks' className="header-butt" activeStyle={{ opacity: 0.3 }}>Задачи</NavLink>
          </li>
          <li className="flex-grow-elem major-grow">
            <NavLink to='/categories' className="header-butt" activeStyle={{ opacity: 0.3 }}>Категории</NavLink>
          </li>
          <li className="flex-grow-elem">
            <button onClick={handleClickAdd} className="header-butt">
              Добавить {currentList === 'Items' ? 'задачу' : 'категорию'}
            </button>
          </li>
        </ul>          
      </nav>
     
    </header>    
  )    
}
  
export default Header