const url = `https://localhost:44377/odata`;

const vm = new Vue({
    el: '#app',
    data:{
        movies: [],
        cinemas: [], 
    },
    async mounted(){
    await axios.get(`${url}/Movies`).then(response =>{
        this.movies = response.data.value
    });
    await axios.get(`${url}/Cinemas`).then(response =>{
        this.cinemas = response.data.value
    })

    },
    // methods:{
    //    async delete_user(index){
    //     // let id = this.results[index].id
    //     // await axios.delete(url + `/${id}`).then(res =>{
    //     //     console.log(res);
    //     // })
    //         this.results.splice(index,1)
    //     }
    // }
});
