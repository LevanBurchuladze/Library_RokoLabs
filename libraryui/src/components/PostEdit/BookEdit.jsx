import Multiselect from "multiselect-react-dropdown";
import React, { useEffect, useState } from "react";
import PostService from "../../API/PostService";
import MyButton from "../UI/Buttons/MyButton";
import MyInput from "../UI/Inputs/MyInput";

const BookEdit = (props) => {
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
    const [useAuthors, setUseAuthors] = useState('');

    useEffect(() => {
        downloadAuthors();
        fetchPost();
    },[])

    async function downloadAuthors(){
        const response =  await PostService.getAuthors();
        const authors = response.map(value =>({
            id: value.authorId,
            name: value.firstName + " " + value.secondName
        }))
        setAuthors(response);
        setAuthorsList(authors);
    }

    async function fetchPost(){
        if(props.postinfo.post.type === 1){
            const post = await PostService.getPostBook(props.postinfo.post.editionId);
            setTitle(post.title);
            setpubYear(post.publicationYear);
            setpubPlace(post.publicationPlace);
            setpubHouse(post.publicationHouse);
            setisbn(post.isbn);
            setdescription(post.description);
            setcountPages(post.countPages);
            setSelectedAuthorsIds(post.authors);
            const authors = post.authors.map(value =>({
                id: value.authorId,
                name: value.firstName + " " + value.secondName
            }))
            setUseAuthors(authors);
        }
    }

    const editBook = (e) =>{
        e.preventDefault();
        const authors = authorsList.filter(author => selectedAuthorsIds.includes(author.authorId));
        const updateBook = {
            "editionId": props.postinfo.post.editionId,
            "title": title,
            "publicationPlace": publicationPlace,
            "publicationYear": publicationYear,
            "countPages": countPages,
            "description": description, 
            "publicationHouse": publicationHouse,
            "isbn": isbn,
            "authors":[],
            "authors": authors
        };
        const jsonBook = JSON.stringify(updateBook);
        const res = uploadEditBook(jsonBook,props.postinfo.post.editionId);
        console.log(res);
    }

    async function uploadEditBook(book,id){
        return await PostService.editBook(book,id);
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
                selectedValues= {useAuthors}
            />

            <MyButton onClick={editBook}>Save book</MyButton>
        </form>
    );
};

export default BookEdit;