import React, { useEffect, useState } from "react";
import PostService from "../../API/PostService";
import MyButton from "../UI/Buttons/MyButton";
import MyInput from "../UI/Inputs/MyInput";

const PatentEdit = (props) => {
    const [title, setTitle] = useState('');
    const [publicationPlace, setpubPlace] = useState('');
    const [publicationYear, setpubYear] = useState('');
    const [countPages, setcountPages] = useState('');
    const [description, setdescription] = useState('');
    const [appDate, setappDate] = useState('');
    const [publicationDate, setpubDate] = useState('');
    const [authors, setAuthors] = useState('');
    const [regNumber, setregNumber] = useState('');

    const editPatent = (e) => {
        e.preventDefault();
        const newPatent = {
            "editionId": props.postinfo.post.editionId,
            "title": title,
            "publicationPlace": publicationPlace,
            "publicationYear": publicationYear,
            "countPages": countPages,
            "description": description,
            "regNumber": regNumber,
            "publicationDate": publicationDate,
            "appDate": appDate, 
            "authors":[]
        };
        const jsonPatent = JSON.stringify(newPatent);
        const res = uploadEditPatent(jsonPatent,props.postinfo.post.editionId);
        console.log(res);
    }

    async function uploadEditPatent(patent,id){
        return await PostService.editPatent(patent,id);
    }

    useEffect(() => {
        fetchPost()
    },[])

    async function fetchPost(){
        if(props.postinfo.post.type === 3){
            const post = await PostService.getPostPatent(props.postinfo.post.editionId);
            setTitle(post.title);
            setpubPlace(post.publicationPlace);
            setpubYear(post.publicationYear);
            setcountPages(post.countPages);
            setdescription(post.description);
            setappDate(convertDate(post.appDate));
            setpubDate(convertDate(post.publicationDate));
            setregNumber(post.regNumber);
        }
    }

    function convertDate(date){
        date = date.split('T');
        const [year, month, day] = date[0].split('-');
        const cDate = `${year}-${month}-${day}`;
        return cDate;
    }

    return (
        <form>
            <label>Title</label>
            <MyInput onChange={e => setTitle(e.target.value)} value={title} type="text" placeholder="Российский телефон"/>
            
            <label>Publication place</label>
            <MyInput onChange={e => setpubPlace(e.target.value)} value={publicationPlace} type="text" placeholder="Россия"/>

            <label>Registration number</label>
            <MyInput onChange={e => setregNumber(e.target.value)} value={regNumber} type="text" placeholder="228"/>
            
            <label>Application date</label>
            <MyInput onChange={e => setappDate(e.target.value)} value={appDate} type="date" placeholder="2018-01-01"/>

            <label>Publication date</label>
            <MyInput onChange={e => setpubDate(e.target.value)} value={publicationDate} type="date" placeholder=""/>

            <label>Publication year</label>
            <MyInput onChange={e => setpubYear(e.target.value)} value={publicationYear} type="text" placeholder="2010"/>

            <label>Count pages</label>
            <MyInput onChange={e => setcountPages(e.target.value)} value={countPages} type="text" placeholder="200"/>

            <label>Description</label>
            <MyInput onChange={e => setdescription(e.target.value)} value={description} type="text" placeholder="О чем-то"/>

            <label>Authors</label>
            <div>
                <select>

                </select>
            </div>

            <MyButton onClick={editPatent}>Save patent</MyButton>
        </form>
    );
};

export default PatentEdit;