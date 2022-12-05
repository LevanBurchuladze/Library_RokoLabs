import React, { useEffect, useState } from "react";
import PostService from "../../API/PostService";
import MyButton from "../UI/Buttons/MyButton";
import MyInput from "../UI/Inputs/MyInput";
import { Multiselect } from "multiselect-react-dropdown";

const PostFormBook = () => {
    const [title, setTitle] = useState('');
    const [publicationPlace, setpubPlace] = useState('');
    const [publicationHouse, setpubHouse] = useState('');
    const [publicationYear, setpubYear] = useState('');
    const [countPages, setcountPages] = useState('');
    const [description, setdescription] = useState('');
    const [isbn, setisbn] = useState('');
    const [authorsList, setAuthors] = useState('');
    const [authorListSelect, setAuthorsList] = useState([]);
    const [selectedAuthorsIds, setSelectedAuthorsIds] = useState('');

    useEffect(() => {
        downloadAuthors();
    },[])
 
    const addBook = (e) => {
        e.preventDefault();
        const authors = authorsList.filter(author => selectedAuthorsIds.includes(author.authorId));
        const newBook = {
            "title": title,
            "type": 1,
            "publicationPlace": publicationPlace,
            "publicationYear": publicationYear,
            "countPages": countPages,
            "description": description, 
            "publicationHouse": publicationHouse,
            "isbn": isbn,
            "authors": authors
        };
        const jsonBook = JSON.stringify(newBook);
        const res = uploadNewBook(jsonBook);
        console.log(res);
    }

    async function uploadNewBook(book){
        return await PostService.createBook(book);
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
            <MyInput onChange={e => setTitle(e.target.value)} value={title} type="text" placeholder="Капитанская дочка"/>
            
            <label>Publication place</label>
            <MyInput onChange={e => setpubPlace(e.target.value)} value={publicationPlace} type="text" placeholder="Москва"/>

            <label>Publication house</label>
            <MyInput onChange={e => setpubHouse(e.target.value)} value={publicationHouse} type="text" placeholder="Аванта"/>
            
            <label>Publication year</label>
            <MyInput onChange={e => setpubYear(e.target.value)} value={publicationYear} type="text" placeholder="2001"/>

            <label>Count pages</label>
            <MyInput onChange={e => setcountPages(e.target.value)} value={countPages} type="text" placeholder="200"/>

            <label>Description</label>
            <MyInput onChange={e => setdescription(e.target.value)} value={description} type="text" placeholder="О чем-то..."/>

            <label>ISBN</label>
            <MyInput onChange={e => setisbn(e.target.value)} value={isbn} type="text" placeholder="ISBN 0-12345-678-9"/>

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

            <MyButton onClick={addBook}>Create book</MyButton>
        </form>
    );
};

export default PostFormBook;