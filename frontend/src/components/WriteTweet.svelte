<script>
    import keycloak from '../keycloak.js';
    import { client } from '../Api.js';
    import axios from 'axios';
    import { onMount } from 'svelte';
    import { Router, Link, Route } from "svelte-routing";

    const submitKweet = async () => {
        // get text from textarea
        const tweetbody = document.getElementById('tweetbody').value;

        if(tweetbody.length < 1) {
            document.getElementById('tweet-error').classList.remove('hidden');
            document.getElementById('tweet-error').innerHTML = "Kweet cannot be empty";
            return;
        }
        
        // send kweet to backend using keyclaok token as bearer token and Axios
        const token = keycloak.token;
        console.log()


        // send kweet to backend through ocelot gateway using keyclaok token as bearer token and avoiding cors
        // also set credentials to true to allow cookies to be sent
        
        axios({
            method: 'POST',
            url: `http://localhost/tweet`,
            withCredentials: false,
            data: {
                tweetBody: tweetbody,
                userName: "null"
            },
            headers: {
                Authorization: `Bearer ${token}`
            }
        }).then((response) => {
            console.log(response);
        }).catch((error) => {
            console.log(error);
        });
    }

    // get text from textarea


    // send kweet to backend using keyclaok token as bearer token


</script>
<!-- form to allow users to write tweets -->
<div>
    <div class="border-b border-gray-100">
        <div class="flex flex-col mt-4">
            <div id="tweet-error" class="text-red-700 hidden">...</div>
            <textarea required minlength="1" id="tweetbody" class="border border-gray-300 rounded-lg p-4 mb-4" placeholder="What's happening?"></textarea>
            <button on:click={submitKweet} id="tweetbtn" class="bg-blue-500 text-white rounded-lg p-4">Kweet</button>
        </div>
    </div>
    <!-- list of tweets -->
</div>