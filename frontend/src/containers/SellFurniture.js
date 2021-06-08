import React from 'react'
import Modal from 'react-bootstrap/Modal';
import DateField from '../components/inputs/DateField';
import Price from '../components/inputs/Price';
import { GetHeaders } from '../services/authService';
import { useState } from 'react';
import axios from 'axios';
import SubmitButton from '../components/inputs/SubmitButton';
import { withRouter } from 'react-router';
import { connect } from 'react-redux';

class initialSale {
    constructor(id){
        this.furnitureId = id;
        this.finalPrice = 0.00;
        this.dateSold = Date.now();
    }
}

const SellFurniture = props => {
    const { id, deleteFromDom, auth } = props;
    const [show, setShow] = useState(false);
    const [sale, setSale] = useState(() => new initialSale(id))

    const handleClose = () => setShow(false);
    const handleOpen = () => setShow(true);

    const sellFurniture = e => {
        e.preventDefault();
        axios.patch(`http://localhost:5000/api/furniture/sell`, sale, GetHeaders(auth))
            .then(results => {
                    console.log(results);
                    deleteFromDom(id);
                    handleClose();
            })
            .catch(err => console.log(err));
    }

    const dateHandler = e => {
        const { name, value } = e.target;

        setSale({
            ...sale,
            [name]: value
        })
    }

    const priceHandler = e => {
        const { name, value } = e.target;
        console.log(value);
        console.log(parseFloat(value));
        setSale({
            ...sale,
            [name]: parseFloat(value)
        })
    }

    return (
        <>
        <button onClick={ handleOpen }>Mark as Sold</button>

        <Modal className="my-modal" show={show} onHide={handleClose}>
            <Modal.Title closeButton>
                <Modal.Title>Furniture Sold!</Modal.Title>
            </Modal.Title>
            <Modal.Body>
                <form onSubmit={sellFurniture}>
                    <Price 
                        name="finalPrice"
                        label="Final Price: "
                        value={sale.finalPrice}
                        onChange={priceHandler}
                    />
                    <DateField
                        name="dateSold"
                        label="Date Sold: "
                        value={sale.dateSold}
                        onChange={dateHandler}
                    />
                    <SubmitButton text="Sell" />
                </form>
            </Modal.Body>
        </Modal>
        </>
    )
}

const mapStateToProps = state => {
    return {
        auth: state.auth
    }
}

export default withRouter(connect(mapStateToProps)(SellFurniture));
