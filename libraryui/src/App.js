import React, {useEffect, useState} from "react";
import PostService from "./API/PostService";
import './styles/App.css'
import PostList from "./components/PostList";
import Header from "./Header";
import Loader from "./components/UI/Loading/Loader";
import ScrollToBack from "./components/ScrollTo/ScrollToBack";
import { getPageCount, getPagesArray } from "./utils/pages";
import MyButton from "./components/UI/Buttons/MyButton";
import Pagination from "./components/UI/Pagination/Pagination";

function App(){
    const [posts,setPosts] = useState([]);
    const [isPostsLoading, setIsPostsLoading] = useState(false);
    const [totalPages,setTotalPages] = useState(1);
    const [page,setPage] = useState(1);

    useEffect(() =>{
        fetchPosts()
    },[page])

    const changePage = (page) =>{
        setPage(page);
    }

    async function fetchPosts(){
        setIsPostsLoading(true);
        setPosts(await PostService.getPagePosts(page));
        setTotalPages(getPageCount(await PostService.getTotalCount()));
        setIsPostsLoading(false);
    }
    
    return (
        <div className="App"> 
            <ScrollToBack>
                <Header/>
                {isPostsLoading
                    ?<div style={{display:'flex', justifyContent: 'center',marginTop:50}}><Loader/></div>
                    :posts.length !==0
                        ?<PostList posts={posts}/>
                        :<h2>Editions have not yet been added, additions!</h2>
                }
                <Pagination 
                    page={page} 
                    changePage={changePage} 
                    totalPages={totalPages}
                />
            </ScrollToBack>
        </div>
    );
}

export default App;