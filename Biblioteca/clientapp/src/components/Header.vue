<template>
    <header class="header">
        <div>
            <router-link to="/">
                <h1 class="logo">Livraria</h1>
            </router-link>
        </div>
        <div id="nav">
            <router-link to="/">Home</router-link> |
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

        .header a {
            color: white;
            font-size: 1.3rem;
            margin: 0 15px;
            font-weight: bold;
            text-decoration: none;
        }


    #nav a.router-link-exact-active {
        color: #00a2e5;
        border-bottom: double;
    }

    .logo{
        margin:0;
        padding:0;
    }

</style>