<template>
   <b-container>
       <h3 class="list-title">Фильмы</h3>
       <b-row>
           <template>
               <b-col cols="3" v-for="(movie, key) in movies" :key="key">
                   <MovieItem :movie="movie"
                   @showModal ="onShowMovieInfo"
                   @purchaseModal ="onPurchaseItem"
                   />
               </b-col>
           </template>
       </b-row>
       <b-modal body-class="movie-modal-body" :id="movieInfoModalID" size="xl" hide-footer hide-header>
           <MovieModalContent :movie="selectedMovie" @closeModal="onCloseModal"/>
        </b-modal>
        <b-modal body-class="movie-modal-body" :id="moviePurchaseID" size="xl" hide-footer hide-header>
            <PurchaseModalContent :movie="puchaseMovie"/>
        </b-modal>
   </b-container>
</template>
<script>
import MovieItem from './MovieItem';
import MovieModalContent from './MovieModalContent';
import PurchaseModalContent from './PurchaseModalContent';
import axios from "axios";
const url = "https://localhost:44319/odata/Movies"
export default {
    name: 'MoviesList',
    data() {
    return {
        movies: [],
        movieInfoModalID: 'movie-info',
        selectedMovieID: '',
        moviePurchaseID: 'purchase-movie',
        selectedMoviePurchaseID: '',
    };
  },
  components: {
    MovieItem,
    MovieModalContent,
    PurchaseModalContent
  },
  computed:{
      selectedMovie(){
          return this.selectedMovieID ? this.movies[this.selectedMovieID] : null;
      },
      puchaseMovie(){
          return this.selectedMoviePurchaseID ? this.movies[this.selectedMoviePurchaseID] : null
      }
  },
  methods:{
      onShowMovieInfo(id){
          console.log(id)
          this.selectedMovieID = id;
         this.$bvModal.show(this.movieInfoModalID);
      },
      onCloseModal(){
          this.selectedMovieID = null;
          this.$bvModal.hide(this.movieInfoModalID);
      },
      onPurchaseItem(){
          this.selectedMoviePurchaseID = null;
          this.$bvModal.hide(this.moviePurchaseID);
      }

  },

  async mounted() {
    await axios.get(url).then((response) => {
      this.movies = response.data.value;
    });
  },
}
</script>
<style scoped>
.list-title{
    font-size: 50px;
    margin-bottom: 30px;
}

</style>

<style>
.movie-modal-body {
  padding: 0 !important;
}
</style>
