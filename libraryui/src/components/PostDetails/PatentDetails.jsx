import React, { useEffect, useState } from "react";
import PostService from "../../API/PostService";
import classes from "./Details.css";

const PatentDetails = (props) => {
    const [postDetails,setPost] = useState([]);

    useEffect(() => {
        fetchPost()
    },[])

    async function fetchPost(){
        if(props.postinfo.post.type === 3){
            const post = await PostService.getPostPatent(props.postinfo.post.editionId);
            setPost(post);
        }
    }

    function stringAuthors(){
        var strAuthors = "";
        var author = "";
        for (let i in postDetails.authors){
            if( i+1 < postDetails.authors.length){
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

            <p>Registration number: <label>{postDetails.regNumber}</label></p>
            
            <p>Application date: <label>{postDetails.appDate}</label></p>

            <p>Publication date: <label>{postDetails.publicationDate}</label></p>

            <p>Publication year: <label>{postDetails.publicationYear}</label></p>

            <p>Count pages: <label>{postDetails.countPages}</label></p>

            <p>Description: <label>{postDetails.description}</label></p>

            <p>Authors: <label>{stringAuthors()}</label></p>
        </form>
    );
};

export default PatentDetails;