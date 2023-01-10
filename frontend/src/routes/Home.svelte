<script>
import keycloak from '../keycloak.js';
import { client } from '../Api.js';
import axios from 'axios';
import WriteTweet from '../components/WriteTweet.svelte';
import { onMount } from 'svelte';
import { once } from 'svelte/internal';
import { Router, Link, Route } from "svelte-routing";
import Time from 'svelte-time';

let timeline;
// create a promise method that waits for the keycloak token to be set
const waitForToken = () => {
    return new Promise((resolve, reject) => {
        const interval = setInterval(() => {
            if(keycloak.token) {
                clearInterval(interval);
                resolve();
            }
        }, 100);
    });
}

onMount(async () => {
    

    waitForToken().then(() => {
        client.get(`home-timeline` , {
        headers: {
            Authorization: `Bearer ${keycloak.token}`
        }
        })
        .then(res => timeline = res)
        .catch(err => console.log(err));
    });

});
</script>

<main class="flex flex-col">
    <div class="py-8 px-[50px] border-b border-gray-100">
        <h1 class="font-bold text-2xl">Startpagina</h1>
        <WriteTweet />
    </div>
    <!-- list of tweets -->
    <div>
        <!-- make a svelte foreach that does 8 iterations -->
        {#if timeline}
            {#each timeline.data.kweets as kweet}
            <div class="py-8 px-[50px] border-b border-gray-100 flex">
                <div class="rounded-full border-8 border-white bg-slate-900 w-12 h-12 mr-4"></div>
                <div class="flex-1">
                <Link to={`/profile/${kweet.userName}`}>
                    <div class="flex justify-between">
                        <div class="font-bold text-lg">{kweet.userName}</div>
                        <Time relative timestamp={kweet.createdAt} />
                        <!-- human readable day (seconds or minutes ago) -->
                    </div>
                </Link>
                
                <div class="mt-2">
                    {kweet.tweetBody}
                </div>
                </div>
            </div>
            {/each}
        {:else}
        {#each [1,2,3,4,5,6,7,8] as tweet}
        <div class="py-8 px-[50px] border-b border-gray-100 flex">
            <div class="skeleton rounded-full w-12 h-12 mr-4"></div>
            <div class="flex-1">
            <div class="flex justify-between">
                <div class="text-gray-500 skeleton w-1/2 h-6 mb-5"></div>
            </div>
            <div class="text-gray-500 skeleton w-full h-14"></div>
            </div>
        </div>
        {/each}
        {/if}
    </div>
</main>

<style>
</style>