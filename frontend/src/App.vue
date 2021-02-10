<template>
  <div id="app">
     <Header/>
    <MoviesList/>
    <Movie/>
  </div>
</template>

<script>
import Header from './components/Header';
import MoviesList from './components/MoviesList';
import Movie from './components/Movie';

export default {
  name: 'App',
  components: {
    Header,
    Movie,
    MoviesList
  }
}
</script>

<style>
@import url(https://fonts.googleapis.com/css?family=Open+Sans);
#app {
  font-family: "Libre Baskerville", serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>
