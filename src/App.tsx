import React, { useEffect } from 'react'
import List from './features/main/List'
import ModalRoot from './features/modals/ModalRoot'
import { Redirect, Route, Switch } from 'react-router-dom';
import { useDispatch } from 'react-redux'
import { getCategories } from './features/categories/categoriesSlice'
import { getItems } from './features/items/itemsSlice'


function App() {
  const dispatch = useDispatch();
  
  useEffect(() => {
    dispatch(getCategories());
    dispatch(getItems());
  }, []);

  return (
    <div className="App">      
      <Switch>
        <Route path='/categories' render={() => (<List currentList={'Catgories'}/>)}/>
        <Route path='/tasks' render={() => (<List currentList={'Items'}/>)}/>
        <Redirect to='/tasks'/>
      </Switch>
      <ModalRoot />
    </div>
  )
}

export default App
