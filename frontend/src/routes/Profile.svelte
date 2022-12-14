<script>
import keycloak from '../keycloak.js';
import { Api } from '../api.js';
import Axios from 'axios';
import { onMount } from 'svelte';
import { Router, Link, Route } from "svelte-routing";
export let username;
let tweets = [];
    const api = new Api('http://localhost/');
    console.log(api);

    onMount(async () => {
        await api.get(`user-timeline/${username}`).then(res => tweets = res.data.items).catch(err => console.log(err));
        console.log(tweets);
    });
    
</script>

<main class="text-left">
    {#each tweets as tweet}
    <div class="py-8 px-[50px] border-b border-gray-100 flex">
        <div class="bg-slate-900 overflow-hidden rounded-full w-12 h-12 mr-4"></div>
        <div class="flex-1">
            <div class="flex justify-between">
                <div class="font-bold w-1/2 h-6 mb-2">{tweet.userName}</div>
            </div>
            <div class="text-slate-900 w-full h-14">{tweet.tweetBody}</div>
        </div>
    </div>
    {/each}
    
</main>
