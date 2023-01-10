<script>
    import keycloak from '../keycloak.js';
    import { client } from '../Api.js';
    import Axios from 'axios';
    import { onMount } from 'svelte';
    import { Router, Link, Route } from "svelte-routing";
    import axios from 'axios';
    
    let claims;
    export let username;
    let tweets = [];
    let followers = [];
    
    onMount(async () => {
        claims = keycloak.tokenParsed;
        
        client.get(`user-timeline/${username}`).then(res => tweets = res.data.items).catch(err => console.log(err));
        client.get(`social/follow/${username}`).then(res => followers = res.data).catch(err => console.log(err));
    });

    const followUser = async () => {
        const token = keycloak.token;
        axios({
            method: 'POST',
            url: `http://localhost/social/follow`,
            withCredentials: false,
            data: username,
            headers: {
                Authorization: `Bearer ${token}`,
                'Content-Type': 'application/json'	
            }
        }).then((response) => {
            console.log(response);
        }).catch((error) => {
            console.log(error);
        });
    }
        
    </script>
    
    <main class="text-left">
        <!-- Profile section -->
        <div>
            <div class="flex justify-between relative flex-col">
                <div class="bg-slate-200 absolute h-[200px] w-full"></div>
                <div class="bg-white h-[200px] w-full mt-[200px] px-[50px] z-10 relative border-b-2 border-gray-100">
                    <!-- profile picture -->
                    <div class="rounded-full border-8 border-white  w-28 h-28 bg-slate-900 mt-[-50px]">
    
                    </div>
                    <div class="flex justify-between w-full">
                        <div>
                            <div class="text-xl font-bold capitalize">{ username }</div>
                            {#if username != null}
                            <div class="text-gray-500">@{ username }</div>
                            {/if}
                        </div>
                        <div>
                            <button on:click={followUser} class="bg-blue-500 text-white font-bold py-2 px-4 rounded-full">Follow</button>
                        </div>
                    </div>
                    <div class="flex mt-3">
                        <div class="flex">
                            {#if followers['followers'] != null}
                            <div class="font-bold mr-1">{followers['followers']}</div>
                            {/if}
                            <div class="text-gray-500 text-sm">Following</div>
                        </div>
                        <div class="flex ml-4">
                            {#if followers['following'] != null}
                            <div class="font-bold mr-1">{followers['following']}</div>
                            {/if}
                            <div class="text-gray-500 text-sm">Following</div>
                        </div>
                    </div>
                </div>
    
            </div>
        </div>
        <!-- timeline section -->
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
    