import Multiselect from "multiselect-react-dropdown";
import React, { useEffect, useState } from "react";
import PostService from "../../API/PostService";
import MyButton from "../UI/Buttons/MyButton";
import MyInput from "../UI/Inputs/MyInput";

const PostFormPatent = () => {
    const [title, setTitle] = useState('');
    const [publicationPlace, setpubPlace] = useState('');
    const [publicationYear, setpubYear] = useState('');
    const [countPages, setcountPages] = useState('');
    const [description, setdescription] = useState('');
    const [appDate, setappDate] = useState('');
    const [publicationDate, setpubDate] = useState('');
    const [regNumber, setregNumber] = useState('');
    const [authorsList, setAuthors] = useState('');
    const [authorListSelect, setAuthorsList] = useState([]);
    const [selectedAuthorsIds, setSelectedAuthorsIds] = useState('');

    useEffect(() => {
        downloadAuthors();
    },[])

    const addPatent = (e) => {
        e.preventDefault();
        const authors = authorsList.filter(author => selectedAuthorsIds.includes(author.authorId));
        const newPatent = {
            "title": title,
            "type": 3,
            "publicationPlace": publicationPlace,
            "publicationYear": publicationYear,
            "countPages": countPages,
            "description": description,
            "regNumber": regNumber,
            "publicationDate": publicationDate,
            "appDate": appDate, 
            "authors": authors
        };
        const jsonPatent = JSON.stringify(newPatent);
        const res = uploadNewPatent(jsonPatent);
        console.log(res);
    }

    async function uploadNewPatent(patent){
        return await PostService.createPatent(patent);
    }

    async function downloadAuthors(){
        const response =  await PostService.getAuthors();
        const authors = response.map(value =>({
            id: value.authorId,
            name: value.firstName + " " + value.secondName
        }))
        setAuthors(response);
        setAuthorsList(authors);
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
            <Multiselect 
                className="multiselect"
                options={authorListSelect} 
                displayValue="name"
                onSelect={(e) =>setSelectedAuthorsIds(e.map(item => item.id))}
                onRemove={(e) =>setSelectedAuthorsIds(e.map(item => item.id))}
                placeholder="Select Author"
                closeIcon="circle"
            />

            <MyButton onClick={addPatent}>Create patent</MyButton>
        </form>
    );
};

export default PostFormPatent;