import React, { useState, FC, ReactElement } from 'react'
import { useSelector, useDispatch } from 'react-redux'
import ItemsElem from '../items/ItemsElem'
import CategoriesElem from '../categories/CategoriesElem'
import { current } from '@reduxjs/toolkit'
import { showModal, hideModal } from '../modals/modalsSlice'
import ModalRoot from '../modals/ModalRoot'
import Header from '../header/Header'
import { itemService } from '../../db/ItemService'
import { categoryService } from '../../db/CategorieService'
import { selectTodos } from '../items/itemsSlice'
import { selectCategories } from '../categories/categoriesSlice'
import { useAppDispatch, useAppSelector } from '../../hooks'
import { Item, Category } from '../../types'

const List:FC<{ currentList: string }> = ({ currentList })  => {

  const items = useAppSelector(selectTodos) as number[];
  const categories = useAppSelector(selectCategories) as number[];

  let renderedList;
  if (currentList === 'Items')
  {
    renderedList = items.map((item) => 
      <ItemsElem key={item} todo={item}/>
    )
  }
  else 
  {
    renderedList = categories.map((category) => 
      <CategoriesElem key={category} todo={category} />
    )  
  }
  
  return (
    <>
      <Header currentList={currentList}/>
      <main className="layout-center main-block">
        <div id="list">{renderedList}</div>    
      </main>
    </>
  )
}

export default List; 