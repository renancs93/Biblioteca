<template>
    <header class="header">
        <div>
            <h1>Livraria</h1>
        </div>
        <div id="nav">
            <router-link to="/">Home</router-link> |
            <!-- <router-link to="/about">About</router-link> | -->
            <router-link to="/autores">Autores</router-link> |
            <router-link to="/livros">Livros</router-link>
        </div>

    </header>
</template>

<script>
    export default {
        name: 'Header',
    }
</script>

<style>

    .header {
        background-color: #ac7339;
        padding: 10px;
        color: white;
        display: flex;
        justify-content: space-between;
        align-items: center;
    }

    .header a{
        color: white;
        font-size: 1.3rem;
        margin: 0 15px;
        font-weight: bold;
    }

    
    #nav a.router-link-exact-active {
        color: #00a2e5;
    }

</style>