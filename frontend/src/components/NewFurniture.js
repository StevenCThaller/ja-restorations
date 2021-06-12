import {useState} from 'react'
import Modal from 'react-bootstrap/Modal';
import {useHistory} from 'react-router-dom';

const NewFurniture = ({title, body, show, setShow}) => {
    const history = useHistory();
    const handleClose = () => {
        console.log(history);
        setShow(false)
    };

    return (
        <Modal className="my-modal" show={show} onHide={handleClose} >
            <Modal.Header closeButton>
                <Modal.Title>{title}</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                {body}
            </Modal.Body>
        </Modal>
    )
}

export default NewFurniture
