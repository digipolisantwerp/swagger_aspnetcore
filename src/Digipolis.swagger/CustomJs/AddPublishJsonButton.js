(function () {
        docReady(addButton);

        function downloadJson() {
            //first get URL from swagger json
            let mainWrapper = document.getElementsByClassName('main')[0];
            let link = mainWrapper.getElementsByTagName('a')[0];

            let request = new XMLHttpRequest();
            request.open('GET', link.href);
            request.responseType = 'json';

            request.onload = function() {
                let definition = request.response;
                
                let regex = /(v)\d*[\/]/;
                //remove versionfrom paths
                for (let p in definition.paths) {
                    let match = p.match(regex);
                    if (match.length > 0) {
                        Object.defineProperty(definition.paths, p.replace(match[0], ''),
                            Object.getOwnPropertyDescriptor(definition.paths, p));
                        delete definition.paths[p];
                    }
                }
                //download json file
                download(JSON.stringify(definition, null, 2));
            };

            request.send();
            
        }

        function download(text) {
            let element = document.createElement('a');
            element.setAttribute('href', 'data:text/plain;charset=utf-8,' + encodeURIComponent(text));
            element.setAttribute('download', 'Swagger.json');

            element.style.display = 'none';
            document.body.appendChild(element);

            element.click();

            document.body.removeChild(element);
        }

        function addButton() {
            let mainWrapper = document.getElementsByClassName('main')[0];
            
            if (!mainWrapper) {
                //if no main is found than the swagger is probably still loading, check again after 250 ms
                setTimeout(addButton, 250);
                return;
            }
            debugger;
            
            mainWrapper.innerHTML += '<button id="downloadPublishJson" class="btn" style="float: right;"><span>Download publish JSON</span></button>';
            
            let button = document.getElementById('downloadPublishJson');
            button.onclick = downloadJson;
        }

        function docReady(fn) {
            // see if DOM is already available
            if (document.readyState === "complete" || document.readyState === "interactive") {
                // call on next available tick
                setTimeout(fn, 1);
            } else {
                setTimeout(() => docReady(fn), 100);
            }
        }
    }

)();