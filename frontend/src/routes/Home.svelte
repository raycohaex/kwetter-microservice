<script>
import keycloak from '../keycloak.js';
import { client } from '../Api.js';
import axios from 'axios';
import WriteTweet from '../components/WriteTweet.svelte';
import { onMount } from 'svelte';
import { once } from 'svelte/internal';

let test;

onMount(async () => {
    client.get(`tweet/08dad221-95d2-4df7-80ac-06f5f689a28e`).then(res => test = res).catch(err => console.log(err));

    // wait for data to be fetched then console log
});

$: if (test) {
    console.log(test);
}
</script>

<main class="flex flex-col">
    <div class="py-8 px-[50px] border-b border-gray-100">
        <h1 class="font-bold text-2xl">Startpagina</h1>
        <WriteTweet />
    </div>
    <!-- list of tweets -->
    <div>
        <!-- make a svelte foreach that does 8 iterations -->
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
    </div>
</main>

<style>
</style>