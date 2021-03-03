import React from 'react'
import Portal from './Portal'
import { ModalProps } from '../types'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import {
  faTimes
} from '@fortawesome/free-solid-svg-icons';


const Modal: React.FC<{ modal: ModalProps}> = ( {modal, children} ) => {
  const { title, onCancel, onSubmit} = modal;
	return (         
        <Portal>
            <div className="modalOverlay">
                <div className="modalWindow">
                    <div className="modalHeader">
                        <div><h1>{title}</h1></div>
                        <div>
                            <button onClick={onCancel} className="button-icon icon">
                                <FontAwesomeIcon icon={faTimes} size="4x"/>
                            </button>
                        </div>
                    </div>
                    <div className="modalBody">
                        {children}
                    </div>
                    <div className="modalFooter">
                        <button onClick={onSubmit} className="modal-butt primary-butt">Submit</button>
                        <button onClick={onCancel} className="modal-butt secondary-butt">Cancel</button>                        
                    </div>
                </div>
            </div>
        </Portal>
    )
}


export default Modal;
