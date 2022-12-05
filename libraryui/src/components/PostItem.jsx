import React, { useState } from "react";
import PostService from "../API/PostService";
import BookDetails from "./PostDetails/BookDetails";
import NewsPaperDetails from "./PostDetails/NewsPaperDetails";
import PatentDetails from "./PostDetails/PatentDetails";
import BookEdit from "./PostEdit/BookEdit";
import NewsPaperEdit from "./PostEdit/NewsPaperEdit";
import PatentEdit from "./PostEdit/PatentEdit";
import MyButton from "./UI/Buttons/MyButton";
import Modal from "./UI/Modal/Modal";

async function deletePost(id) {
    if (window.confirm(`Are you sure you want to delete this post?`)){
        await PostService.DeleteById(id);
        window.location.reload();
    }
}

const PostItem = (props) =>{

    const [details, setModalDetails] = useState({
        BookDetails:false,
        NewsPaperDetails:false,
        PatentDetails:false,
        BookEdit:false,
        NewsPaperEdit:false,
        PatentEdit:false
    });

    function closeForm(){
        window.location.reload();
    } 

    return(
        <div className="post" >
            <div className="post__content">
                <div>
                    Title: <label>{props.post.title}</label>
                </div>
                <div>
                    Publication Place: <label>{props.post.publicationPlace}</label>
                </div>
                <div>
                    Description: <label>{props.post.description}</label>
                </div>
                <div>
                    Year: <label>{props.post.publicationYear}</label>
                </div>
            </div>

            <div className="post_btns">
                <button className="post_btns_button" onClick={() => {
                if (props.post.type === 1) {
                    setModalDetails({BookDetails:true});
                }
                else if (props.post.type === 2) {
                    setModalDetails({NewsPaperDetails:true});
                }
                else{
                    setModalDetails({PatentDetails:true})
                }}}>Details</button>
                <button className="post_btns_button" onClick={() => {
                if (props.post.type === 1) {
                    setModalDetails({BookEdit:true});
                }
                else if (props.post.type === 2) {
                    setModalDetails({NewsPaperEdit:true});
                }
                else{
                    setModalDetails({PatentEdit:true})
                }}}>Edit</button>
                <button className="post_btns_button" onClick={ () => deletePost(props.post.editionId)}>Delete</button> 
            </div>

            <Modal visible = {details.BookDetails} setVisible = {setModalDetails}>
                <div onClick={() => setModalDetails({BookDetails:false})}>
                    <MyButton>Close</MyButton>
                </div>
                <h2>Book</h2>
                <BookDetails postinfo={props}></BookDetails>
            </Modal>

            <Modal visible = {details.NewsPaperDetails} setVisible = {setModalDetails}>
                <div onClick={() => setModalDetails({NewsPaperDetails:false})}>
                    <MyButton>Close</MyButton>
                </div>
                <h2>NewsPaper</h2>
                <NewsPaperDetails postinfo={props}></NewsPaperDetails>
            </Modal>
  
            <Modal visible = {details.PatentDetails} setVisible = {setModalDetails}>
                <div onClick={() => setModalDetails({PatentDetails:false})}>
                    <MyButton>Close</MyButton>
                </div>
                <h2>Patent</h2>
                <PatentDetails postinfo={props}></PatentDetails>
            </Modal>

            <Modal visible = {details.PatentEdit} setVisible = {setModalDetails}>
                <div onClick={() => setModalDetails({PatentEdit:false})}>
                    <MyButton onClick={closeForm}>Close</MyButton>
                </div>
                <h2>Edit Patent</h2>
                <PatentEdit postinfo={props}></PatentEdit>
            </Modal>

            <Modal visible = {details.BookEdit} setVisible = {setModalDetails}>
                <div onClick={() => setModalDetails({BookEdit:false})}>
                    <MyButton onClick={closeForm}>Close</MyButton>
                </div>
                <h2>Edit Book</h2>
                <BookEdit postinfo={props}></BookEdit>
            </Modal>

            <Modal visible = {details.NewsPaperEdit} setVisible = {setModalDetails}>
                <div onClick={() => setModalDetails({NewsPaperEdit:false})}>
                    <MyButton onClick={closeForm}>Close</MyButton>
                </div>
                <h2>Edit NewsPaper</h2>
                <NewsPaperEdit postinfo={props}></NewsPaperEdit>
            </Modal>
        </div>
    )
}

export default PostItem;