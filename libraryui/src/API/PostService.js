import axios from "axios";

export default class PostService {
    static async getAll() {
      const response = await axios.get("https://localhost:5001/api/Edition");
      return response.data;
    }
    
    static async DeleteById(id) {
      await axios.delete(`https://localhost:5001/api/Edition/${id}`,{
        'Authorization': 'Bearer' + sessionStorage.getItem("access_token")
      });
    }

    static async createBook(book) {
      console.log(book);
      const response = await axios.post(`https://localhost:5001/api/Book`,book,{
        headers: {
          // Overwrite Axios's automatically set Content-Type
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + sessionStorage.getItem("access_token")
        }
      });
      return response.data;
    }

    static async editBook(book,id){
      const response = await axios.put(`https://localhost:5001/api/Book/${id}`,book,{
        headers: {
          // Overwrite Axios's automatically set Content-Type
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + sessionStorage.getItem("access_token")
        }
      });
      return response.data;
    }

    static async getPostBook(id) {
      const response = await axios.get(`https://localhost:5001/api/Book/${id}`);
      return response.data;
    }

    static async createNewsPaper(newPaper) {
      const response = await axios.post(`https://localhost:5001/api/NewsPaper`,newPaper,{
        headers: {
          // Overwrite Axios's automatically set Content-Type
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + sessionStorage.getItem("access_token")
        }
      });
      return response.data;
    }

    static async editNewsPaper(newsPaper,id){
      const response = await axios.put(`https://localhost:5001/api/NewsPaper/${id}`,newsPaper,{
        headers: {
          // Overwrite Axios's automatically set Content-Type
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + sessionStorage.getItem("access_token")
        }
      });
      return response.data;
    }

    static async getPostNewsPaper(id) {
      const response = await axios.get(`https://localhost:5001/api/NewsPaper/${id}`);
      return response.data;
    }

    static async createPatent(patent) {
      const response = await axios.post(`https://localhost:5001/api/Patent`,patent,{
        headers: {
          // Overwrite Axios's automatically set Content-Type
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + sessionStorage.getItem("access_token")
        }
      });
      return response.data;
    }

    static async editPatent(patent,id){
      const response = await axios.put(`https://localhost:5001/api/Patent/${id}`,patent,{
        headers: {
          // Overwrite Axios's automatically set Content-Type
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + sessionStorage.getItem("access_token")
        }
      });
      return response.data;
    }

    static async getPostPatent(id) {
      const response = await axios.get(`https://localhost:5001/api/Patent/${id}`);
      return response.data;
    }

    static async getAuthors(){
      const response = await axios.get(`https://localhost:5001/api/Author`);
      return response.data;
    }

    static async authorize(login, password){
      const response = await axios.post(`https://localhost:5001/token?login=${login}&password=${password}`);
      return response;
    }

    static async getTotalCount(){
      const response = await axios.get(`https://localhost:5001/api/Edition`);
      return response.data;
    }

    static async getPagePosts(page){
      const response = await axios.get(`https://localhost:5001/api/Edition/${page}`);
      return response.data;
    }
    
}
