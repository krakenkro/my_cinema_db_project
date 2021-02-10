<template>
  <div id="app">
     <Header/>
    <MoviesList/>
  </div>
</template>

<script>
import Header from './components/Header';
import MoviesList from './components/MoviesList';


export default {
  name: 'App',
  components: {
    Header,
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
