import React, { useEffect, useState } from "react";
import PostService from "../../API/PostService";
import classes from "./Details.css";

const BookDetails = (props) => {
    const [postDetails,setPost] = useState([]);

    useEffect(() => {
        fetchPost()
    },[])

    async function fetchPost(){
        if(props.postinfo.post.type === 1){
            const post = await PostService.getPostBook(props.postinfo.post.editionId);
            setPost(post);
        }
    }

    function stringAuthors(){
        var strAuthors = "";
        var author = "";
        for (let i in postDetails.authors){
            if( i < postDetails.authors.length-1){
                author = postDetails.authors[i].firstName + " " + postDetails.authors[i].secondName + ", ";
            }
            else{
                author = postDetails.authors[i].firstName + " " + postDetails.authors[i].secondName;
            }
            strAuthors += author;
        }
        return strAuthors;
    }

    return (
        <form className={classes}>
            <p>Title: <label>{postDetails.title}</label></p>
            
            <p>Publication place: <label>{postDetails.publicationPlace}</label></p>

            <p>Publication house: <label>{postDetails.publicationHouse}</label></p>
            
            <p>Publication year: <label>{postDetails.publicationYear}</label></p>

            <p>Count pages: <label>{postDetails.countPages}</label></p>

            <p>Description: <label>{postDetails.description}</label></p>

            <p>ISBN: <label>{postDetails.isbn}</label></p>

            <p>Authors: <label>{stringAuthors()}</label></p>
        </form>
    );
};

export default BookDetails;