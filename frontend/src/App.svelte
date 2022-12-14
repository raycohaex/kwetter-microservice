<script>
  import { Router, Link, Route } from "svelte-routing";
  import Home from "./routes/Home.svelte";
  import About from "./routes/About.svelte";
  import keycloak from "./keycloak.js";
  
  export let url = ""; //This property is necessary declare to avoid ignore the Router

  // the code below gives a 401 error, how to fix it? 

  keycloak
    .init({ onLoad: "login-required" })
    .then(function (authenticated) {
      alert(authenticated ? "authenticated" : "not authenticated");
    })
    .catch(function (e) {
      alert("failed to initialize");
      console.log(e);
    });

</script>


<main class="bg-slate-100">

  <Router url="{url}">
    <nav>
       <Link to="/">Home</Link>
       <Link to="about">About</Link>
     </nav>
     <div>
       <Route path="about" component="{About}" /> 
       <Route path="/"><Home /></Route>
     </div>
   </Router>

</main>

<style>

</style>
