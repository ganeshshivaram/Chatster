Create new angular project:

npm install -g @angular/cli
ng new <my-app-name>
cd <my-app-name>
ng serve


Useful tools:

angular snippets
angular files
angular language service
auto rename tag
bracket pair colorizer
debugger for chrome
material ui theme
prettier
TSLint
angular2-switcher


Deployment:

1. Modify angular.json -> build -> outputPath
2. In Startup.cs -> Add UseDefaultFiles() and UseStaticFiles()
3. Create fallback controller to serve physical file
4. Check environment.prod.TSLint
