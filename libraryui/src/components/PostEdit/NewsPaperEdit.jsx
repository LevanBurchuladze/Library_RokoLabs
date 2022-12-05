import React, { useEffect, useState } from "react";
import PostService from "../../API/PostService";
import MyButton from "../UI/Buttons/MyButton";
import MyInput from "../UI/Inputs/MyInput";

const NewsPaperEdit = (props) => {
    const [title, setTitle] = useState('');
    const [publicationPlace, setpubPlace] = useState('');
    const [publicationHouse, setpubHouse] = useState('');
    const [publicationYear, setpubYear] = useState('');
    const [countPages, setcountPages] = useState('');
    const [description, setdescription] = useState('');
    const [number,setnumber] = useState('');
    const [date, setdate] = useState('');
    const [issn, setissn] = useState('');

    useEffect(() => {
        fetchPost()
    },[])

    const editNewsPaper = (e) => {
        e.preventDefault();
        const newNewsPaper = {
            "editionId": props.postinfo.post.editionId,
            "title": title,
            "type": 2,
            "publicationPlace": publicationPlace,
            "publicationYear": publicationYear,
            "countPages": 0,
            "description": description,
            "newsPaperId": 0,
            "publicationHouse": publicationHouse,
            "number": 0,
            "date": date,
            "issn": issn
        };
        const jsonNewsPaper = JSON.stringify(newNewsPaper);
        const res = uploadEditNewsPaper(jsonNewsPaper);
        console.log(res);
    }

    async function uploadEditNewsPaper(newsPaper,id){
        return await PostService.editNewsPaper(newsPaper,id);
    }

    async function fetchPost(){
        if(props.postinfo.post.type === 2){
            const post = await PostService.getPostNewsPaper(props.postinfo.post.editionId);
            setTitle(post.newsPaper.title);
            setpubPlace(post.newsPaper.publicationPlace);
            setpubHouse(post.newsPaper.publicationHouse);
            setpubYear(post.newsPaper.publicationYear);
            setcountPages(post.newsPaper.countPages);
            setdescription(post.newsPaper.description);
            setnumber(post.newsPaper.number);
            setdate(convertDate(post.newsPaper.date));
            setissn(post.newsPaper.issn);
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
            <MyInput onChange={e => setTitle(e.target.value)} value={title} type="text" placeholder="Желтая пресса"/>

            <label>Publication place</label>
            <MyInput onChange={e => setpubPlace(e.target.value)} value={publicationPlace} type="text" placeholder="Москва"/>

            <label>Publication house</label>
            <MyInput onChange={e => setpubHouse(e.target.value)} value={publicationHouse} type="text" placeholder="Волга-Пресс"/>

            <label>Publication year</label>
            <MyInput onChange={e => setpubYear(e.target.value)} value={publicationYear} type="text" placeholder="2001"/>

            <label>Count pages</label>
            <MyInput onChange={e => setcountPages(e.target.value)} value={countPages} type="text" placeholder="9"/>

            <label>Description</label>
            <MyInput onChange={e => setdescription(e.target.value)} value={description} type="text" placeholder="О чем-то..."/>

            <label>Number</label>
            <MyInput onChange={e => setnumber(e.target.value)} value={number} type="text" placeholder="31228"/>

            <label>Date</label>
            <MyInput onChange={e => setdate(e.target.value)} value={date} type="date" placeholder="2001-01-01"/>

            <label>ISSN</label>
            <MyInput onChange={e => setissn(e.target.value)} value={issn} type="text" placeholder="ISSN 1234-5678"/>

            <MyButton onClick={editNewsPaper}>Save NewsPaper</MyButton>
        </form>
    );
};

export default NewsPaperEdit;