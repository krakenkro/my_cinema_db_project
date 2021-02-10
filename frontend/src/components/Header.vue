<template>
  <header class="header">
    <BNavbar type="dark" class="navbar" >
      <BContainer>
        <!-- <BNavbarBrand href="#">Cinema</BNavbarBrand> -->
        <b-collapse id="nav-collapse" is-nav>
            <b-navbar-nav>
                <b-nav-item style="font-size: 20px;" href="#">Фильмы</b-nav-item>
                <b-nav-item style="font-size: 20px;" href="#">Кинотеатры</b-nav-item>
            </b-navbar-nav>
        </b-collapse>
        <BNavForm>
          <BFormInput
            class="mr-sm-2 search-input"
            placeholder="Search"
            v-model="searchValue"
            debounce="500"
          ></BFormInput>
        </BNavForm>
      </BContainer>
    </BNavbar>
  </header>
</template>
<script>
export default {
    name: "Header",
    data: ()=>({
        searchValue: ""
    }),
    watch:{
        searchValue: "onSearchValueChanged"
    },
    methods:{
        onSearchValueChanged(val){
            console.log(val)
        }
    }
}
</script>
<style scoped>
.header {
  margin-bottom: 30px;
}

.navbar {
  background-color: #563d7c;
}

.search-input {
  color: #fff;
}

.search-input:focus {
  box-shadow: none;
  color: black;
  border-color: rgba(0, 0, 0, 0.6);
}
</style>
