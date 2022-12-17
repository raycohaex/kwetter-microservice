<script>
  import { Router, Link, Route } from "svelte-routing";
  import Home from "./routes/Home.svelte";
  import Profile from "./routes/Profile.svelte";
  import keycloak from "./keycloak.js";
  import Dashboard from "./layout/Dashboard.svelte";
  import { onMount } from 'svelte';
    import ProfileSelf from "./routes/ProfileSelf.svelte";
  
  export let url = ""; //This property is necessary declare to avoid ignore the Router
  export let preferredUsername;
  // the code below gives a 401 error, how to fix it? 

  keycloak
    .init({ onLoad: "login-required" })
    .then(function (authenticated) {
      if (authenticated) {
        preferredUsername = keycloak.tokenParsed.preferred_username;
      } else {
        console.log("not authenticated");
      }
    })
    .catch(function (e) {
      alert("failed to initialize");
      console.log(e);
    });
</script>

<!-- basic tailwind layout which has a container and a grid with 3 columns -->

<main class="container max-w-[1200px] mx-auto text-sm min-h-screen">
  <Router url="{url}">
    <div class="mx-auto grid grid-cols-5 min-h-screen">
      <div class="col-span-1 border-r-2 border-gray-100">
        <div class="px-7 pt-[80px]">
          <nav class="flex flex-col p-3 text-xl">
            <Link to="/" class="my-2">Home</Link>
            {#if preferredUsername !== undefined}
            <Link to={`profile/${preferredUsername}`}>Profile</Link>
            {/if}
          </nav>
        </div>

      </div>
      <div class="col-span-3">

        <Route path="/"><Home /></Route>
        <Route path="/profile/:username" let:params><Profile username={params.username} /></Route>
        {#if preferredUsername !== undefined}
        <Route path={"/profile/" + preferredUsername}><ProfileSelf username={preferredUsername} /></Route>
        {/if}

      </div>

      <div class="col-span-1 border-l-2 border-gray-100">
        <div class="px-7 pt-[80px]">
          <h2 class="font-bold text-xl">Trending</h2>
        </div>
      </div>
    </div>
  </Router>
</main>

<style>

</style>
