
<template>
    <main class="d-flex align-items-center min-vh-100 py-3 py-md-0">
        <div class="container">

            <div class="card login-card">
                <div class="row">
                    <div class="col-md-6">
                        <img src="/src/assets/images/login.jpg" alt="login" class="login-card-img">
                    </div>

                    <div class="col-md-6">
                        <div class="card-body text-center">

                            <div class="brand-wrapper mt-5">
                                <img width="100" src="/src/assets/images/logo.png" alt="logo" class="logo">
                            </div>

                            <p class="login-card-description">Sign into your account</p>
                            <form @submit.prevent="login" class="was-validated justify-content-center d-inline-block">

                                <div class="form-group mb-2">
                                    <input type="text" required
                                           name="username" v-model.trim="model.userName"
                                           class="form-control" placeholder="User name">
                                </div>
                                <div class="form-group mb-4">
                                    <input type="password" required name="password" v-model.trim="model.password"
                                           class="form-control" placeholder="●●●●●●●●">
                                </div>
                                <input name="login" id="login" class="btn btn-dark login-btn mb-4 w-100" type="submit" value="Login">

                            </form>
                            <nav class="login-card-footer-nav justify-content-between d-flex">
                                <a href="https://norlinq.com/">Terms & Conditions.</a>
                                <a href="https://norlinq.com/">Privacy policy</a>
                            </nav>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>

</template>

<script setup lang="ts">
    import { onMounted, ref } from "vue";
    import type { AccessTokenDto, LoginDto } from "../../core/models/SecurityDtos";
    import Storage from "../../core/services/storage.service"
    import { LoginUrl } from "../../core/appsettings";
    import HttpAppService from '../../core/services/http.service';
    import { useRouter } from "vue-router";

    //Storage.Flush();

    const router = useRouter();

    const model = ref<LoginDto>({ userName: "user", password: "user", rememberMe: true, });


    onMounted(() => {
        Storage.Flush();
    })

    async function login() {

        (await HttpAppService._Post<AccessTokenDto>
            (LoginUrl, model.value)).
            Result(dto => {
                if (dto) {
                    Storage.SetCurrentAccount(dto);
                    if (dto.accessToken) {
                       Storage.SetAccessToken(dto.accessToken);
                        router.push('/');
                    }
                }
            }).Error(err => console.error(err));
      
    }


</script>


<style scoped>

    body {
        background-color: #d0d0ce;
        min-height: 100vh;
    }

    main {
        background-size: cover;
        background-repeat: no-repeat;
        background-image: linear-gradient(rgba(255,255,255,0.5), rgba(255,255,255,0.5)), url('/src/assets/images/login.jpg');
        background-position: 50% 0;
    }

    .brand-wrapper.logo {
        height: 80px;
    }

    .login-card {
        border: 0;
        border-radius: 27.5px;
        box-shadow: 0 10px 30px 0 rgba(172, 168, 168, 0.43);
        overflow: hidden;
    }

    .login-card-img {
        border-radius: 0;
        /* position: absolute;*/
        width: 100%;
        height: 100%;
        -o-object-fit: cover;
        object-fit: cover;
    }

    .login-card.card-body {
        padding: 85px 60px 60px;
    }

    @media(max-width: 422px) {
        .login-card.card-body {
            padding: 35px 24px;
        }
    }

    .login-card-description {
        font-size: 25px;
        color: #000;
        font-weight: normal;
        margin-bottom: 23px;
    }

    .login-card form {
        max-width: 326px;
    }

    .login-card.form-control {
        border: 1px solid #d5dae2;
        padding: 15px 25px;
        margin-bottom: 20px;
        min-height: 45px;
        font-size: 13px;
        /*  line-height: 15;*/
        font-weight: normal;
    }

        .login-card.form-control::-webkit-input-placeholder {
            color: #919aa3;
        }

        .login-card.form-control::-moz-placeholder {
            color: #919aa3;
        }

        .login-card.form-control:-ms-input-placeholder {
            color: #919aa3;
        }

        .login-card.form-control:-ms-input-placeholder {
            color: #919aa3;
        }

        .login-card.form-control::placeholder {
            color: #919aa3;
        }

    .login-card.login-btn {
        padding: 13px 20px 12px;
        background-color: #000;
        border-radius: 4px;
        font-size: 17px;
        font-weight: bold;
        line-height: 20px;
        color: #fff;
        margin-bottom: 24px;
    }

        .login-card.login-btn:hover {
            border: 1px solid #000;
            background-color: transparent;
            color: #000;
        }

    .login-card.forgot-password-link {
        font-size: 14px;
        color: #919aa3;
        margin-bottom: 12px;
    }

    .login-card-footer-text {
        font-size: 16px;
        color: #0d2366;
        margin-bottom: 60px;
    }

    @media(max-width: 767px) {
        .login-card-footer-text {
            margin-bottom: 24px;
        }
    }

    .login-card-footer-nav a {
        font-size: 14px;
        color: #919aa3;
    }
</style>
