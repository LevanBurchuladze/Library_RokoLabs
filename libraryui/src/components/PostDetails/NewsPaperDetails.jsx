import React, { useEffect, useState } from "react";
import PostService from "../../API/PostService";
import classes from "./Details.css";

const NewsPaperDetails = (props) => {
    const [postDetails,setPost] = useState([]);

    useEffect(() => {
        fetchPost()
    },[])

    async function fetchPost(){
        if(props.postinfo.post.type === 2){
            const post = await PostService.getPostNewsPaper(props.postinfo.post.editionId);
            setPost(post.newsPaper);
        }
    }

    return (
        <form className={classes}>
            <p>Title: <label>{postDetails.title}</label></p>
            
            <p>Publication place: <label>{postDetails.publicationPlace}</label></p>

            <p>Publication house: <label>{postDetails.publicationHouse}</label></p>

            <p>Publication year: <label>{postDetails.publicationYear}</label></p>

            <p>Count pages: <label>{postDetails.countPages}</label></p>

            <p>Description: <label>{postDetails.description}</label></p>

            <p>Number: <label>{postDetails.number}</label></p>

            <p>Date: <label>{postDetails.date}</label></p>

            <p>ISSN: <label>{postDetails.issn}</label></p>
        </form>
    );
};

export default NewsPaperDetails;