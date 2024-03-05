
<template>

    <div class="container-fluid p-0 d-flex h-100">

        <div id="mainsidebar"
             class="d-flex flex-column
                    flex-shrink-0 p-3
                    text-white offcanvas-md offcanvas-start">
            <RouterLink to="/" class="navbar-brand text-center">

                <img width="80" src="/src/assets/images/logo.png" />

            </RouterLink>

            <hr>
            <ul class="custnav text-dark nav nav-pills flex-column mb-auto">

                <li class="nav-item mb-1">
                    <RouterLink to="/notes" active-class="active" class="p-3 rounded-2 d-block w-100 text-decoration-none text-dark fs-5" href="#">
                        <i class="me-2 bi bi-people"></i>
                        Notes
                    </RouterLink>
                </li>


                <li class="nav-item mb-1">
                    <RouterLink to="/settings" active-class="active" class="p-3 rounded-2 d-block w-100 text-decoration-none text-dark fs-5" href="#">
                        <i class="me-2 bi bi-gear"></i>
                        Settings
                    </RouterLink>
                </li>




            </ul>

            <hr>

            <div class="d-flex">

                <span>
                    <h6 class="text-dark">
                        {{Storage.GetCurrentAccount()?.email}}
                    </h6>
                </span>
                <i class="bi bi-emoji-smile-fill text-dark ms-2"></i>
            </div>

        </div>

        <div class="bg-light flex-fill">



            <div class="p-4 pt-2">

                <nav class="navbar bg-light p-0">
                    <div class="container-fluid">


                        <div class="p-2 d-md-none d-flex text-dark ">
                            <a href="#" class="text-dark"
                               data-bs-toggle="offcanvas"
                               data-bs-target="#mainsidebar">
                                <i class="bi bi-list"></i>
                            </a>
                            <span class="ms-3">Note Linq App</span>
                        </div>


                        <a class="navbar-brand col">Portal</a>


                        <div class="d-flex align-items-center dropdown">
                            <span class="mx-2">Hi, {{Storage.GetCurrentAccount()?.name}}</span>

                            <button class="btn border-0 dropdown-toggle content"
                                    type="button" data-bs-toggle="dropdown" aria-expanded="false">
                                <i class="fs-3 bi bi-person-circle"></i>
                            </button>
                            <ul class="dropdown-menu dropdown-menu-dark">
                                <li><h6 class="dropdown-header">Profile</h6></li>
                                <li><RouterLink class="dropdown-item" to="/login">logout</RouterLink></li>
                            </ul>

                        </div>

                    </div>
                </nav>

                <hr class="my-2">
                <div class="row">
                    <div class="col">

                        <RouterView />

                    </div>
                </div>
            </div>

        </div>
    </div>
</template>


<script setup lang="ts">
    import { onMounted } from 'vue';
    import { useRouter } from "vue-router";
    import Storage from "../../../core/services/storage.service"


    const router = useRouter();

    if (!Storage.IsAuthorize()) {
        Storage.Flush();
        router.push("login")
    }

</script>

<style>

    html, body {
        height: 100%;
    }


    .custnav li a.active {
        background: rgba(0, 0, 0, 0.2);
    }

    .custnav li a:hover {
        background: rgba(0, 0, 0, 0.2);
        transition: 0.8s
    }


    .dropdown-toggle::after {
        display: none !important;
        content: unset !important;
    }
</style>