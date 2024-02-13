import * as THREE from 'three';
import * as TWEEN from '@tweenjs/tween.js';
import { MapControls } from 'OrbitControls';
import { Sky } from 'Sky';

let orbitControls;
let scene, renderer, camera;
let intersect;

let light, color, intensity;
let sky, sun;

let count = 0;
let floor, groundPlane;
let itemCube;
let objectToIntersect = [];

const group = new THREE.Group()
const jsonGroup = new THREE.Group()

const image = new THREE.ImageLoader()
const file = new THREE.FileLoader();

function init(){

    let canvas = document.getElementById("main")
    scene = new THREE.Scene();
    renderer = new THREE.WebGLRenderer({antialias: true});
    renderer.setSize(window.innerWidth, window.innerHeight);
    canvas.appendChild(renderer.domElement);

    const fov = 35;
    const aspect = window.innerWidth / window.innerHeight;
    const near = 0.1;
    const far = 1000;

    camera = new THREE.PerspectiveCamera(fov, aspect, near, far);
    camera.position.set(0, 125, 0)

    
    orbitControls = new MapControls(camera, renderer.domElement)
    orbitControls.target.set(0, 0.5, 0);
    orbitControls.update();
    orbitControls.minPolarAngle = THREE.MathUtils.degToRad(0);
    orbitControls.maxPolarAngle = THREE.MathUtils.degToRad(90);
    orbitControls.minDistance = 10;
    orbitControls.maxDistance = 1000;
    
    //const light = new THREE.DirectionalLight(0xffffff, 1);
    //light.position.set(0, 10, 10)
    //scene.add(light)

    const floorWidth = 10000;
    const floorHeight = 10000;
    const planeWidthSegments = 4;
    const planeHeightSegments = 4;
    const floorGeometry = new THREE.PlaneGeometry(floorWidth, floorHeight);
    const floorMaterial = new THREE.MeshLambertMaterial({ color: "#8c8c8c" });
    //const floorMaterial = new THREE.MeshBasicMaterial({
    //    color: "#8c8c8c",
    //    side: THREE.DoubleSide,
    //    wireframe: false
    //});
    
    floor = new THREE.Mesh(floorGeometry, floorMaterial);
    floor.position.set(0, -0.5, 0)
    floor.rotation.set(Math.PI/ -2, 0, 0)
    floor.name = "plane";
    scene.add(floor);

    const groundHeight = 100
    const groundWidth = 50

    const groundDivision = (groundHeight * groundWidth) / 2;
    const rollOverGround = (groundHeight * groundWidth) / groundDivision
    const ground = new THREE.PlaneGeometry(groundHeight, groundWidth);
    ground.rotateX(-Math.PI / 2);
    groundPlane = new THREE.Mesh(ground, new THREE.MeshBasicMaterial({ color: "white"}))
    groundPlane.position.set(0, 0, 0);
    groundPlane.name = "groundPlane"
    group.add(groundPlane);
    scene.add(group)

    lights();

    // Sky
    initSky();



    //Dulo left edge
    const wallVer1 = new THREE.Mesh(new THREE.BoxGeometry(2, 2, 22), new THREE.MeshBasicMaterial({color: 0x4672c3}))
    wallVer1.position.set(-19, 1, 0);
    jsonGroup.add(wallVer1)

    //Dulo Right edge
    const wallVer2 = new THREE.Mesh(new THREE.BoxGeometry(2, 2, 22), new THREE.MeshBasicMaterial({color: 0x4672c3}))
    wallVer2.position.set(19, 1, 0);
    jsonGroup.add(wallVer2)


    const wallVer3 = new THREE.Mesh(new THREE.BoxGeometry(2, 2, 6), new THREE.MeshBasicMaterial({color: 0x4672c3}))
    wallVer3.position.set(-5, 1, -7);
    jsonGroup.add(wallVer3)

    const wallVer4 = new THREE.Mesh(new THREE.BoxGeometry(2, 2, 8.5), new THREE.MeshBasicMaterial({color: 0x4672c3}))
    wallVer4.position.set(-5, 1, 2.5);
    jsonGroup.add(wallVer4)

    const wallVer5 = new THREE.Mesh(new THREE.BoxGeometry(2, 2, 9), new THREE.MeshBasicMaterial({color: 0x4672c3}))
    wallVer5.position.set(5, 1, -3.5);
    jsonGroup.add(wallVer5)

    const wallVer6 = new THREE.Mesh(new THREE.BoxGeometry(2, 2, 7.5), new THREE.MeshBasicMaterial({color: 0x4672c3}))
    wallVer6.position.set(5, 1, 7);
    jsonGroup.add(wallVer6)

    const wallVer7 = new THREE.Mesh(new THREE.BoxGeometry(2, 2, 4), new THREE.MeshBasicMaterial({color: 0x4672c3}))
    wallVer7.position.set(13, 1, -6);
    jsonGroup.add(wallVer7)

    const wallVer8 = new THREE.Mesh(new THREE.BoxGeometry(2, 2, 2), new THREE.MeshBasicMaterial({color: 0x4672c3}))
    wallVer8.position.set(-5, 1, 9);
    jsonGroup.add(wallVer8)

    // Top Hor Line
    const wallHor1 = new THREE.Mesh(new THREE.BoxGeometry(33.5, 2, 2), new THREE.MeshBasicMaterial({color: 0x4672c3}))
    wallHor1.position.set(-2, 1, -10);
    jsonGroup.add(wallHor1)

    const wallHor2 = new THREE.Mesh(new THREE.BoxGeometry(26, 2, 2), new THREE.MeshBasicMaterial({color: 0x4672c3}))
    wallHor2.position.set(7, 1, 10);
    jsonGroup.add(wallHor2)

    const wallHor3 = new THREE.Mesh(new THREE.BoxGeometry(16, 2, 2), new THREE.MeshBasicMaterial({color: 0x4672c3}))
    wallHor3.position.set(-10, 1, 5);
    jsonGroup.add(wallHor3)

    const wallHor4 = new THREE.Mesh(new THREE.BoxGeometry(14, 2, 2), new THREE.MeshBasicMaterial({color: 0x4672c3}))
    wallHor4.position.set(12, 1, -4.5);
    jsonGroup.add(wallHor4)

    const wallHor5 = new THREE.Mesh(new THREE.BoxGeometry(5, 2, 2), new THREE.MeshBasicMaterial({color: 0x4672c3}))
    wallHor5.position.set(3, 1, 5);
    jsonGroup.add(wallHor5)
    // Small top left
    const wallHor6 = new THREE.Mesh(new THREE.BoxGeometry(2, 2, 2), new THREE.MeshBasicMaterial({color: 0x4672c3}))
    wallHor6.position.set(18, 1, -10);
    jsonGroup.add(wallHor6)

    function addItemCube(x, y, z, room){
        const itemCubeGeometry = new THREE.BoxGeometry(12, 2, 4);
        const itemCubeMaterial = new THREE.MeshBasicMaterial({ color: 0x71ac4a});

        const itemCube = new THREE.Mesh(itemCubeGeometry, itemCubeMaterial);
        
        itemCube.position.set(x, y, z);
        itemCube.name = "item" + count;
        count++;
        jsonGroup.add(itemCube);
        objectToIntersect.push(itemCube)
        itemCube.userData = {"room": room } 

        const edges = new THREE.EdgesGeometry(itemCubeGeometry)
        const line = new THREE.LineSegments(edges, new THREE.LineBasicMaterial({ color: 0x000000}));
        itemCube.add(line);
        line.name = "line"

        var geo = new THREE.EdgesGeometry(itemCube.itemCubeGeometry)
        var mat = new THREE.LineBasicMaterial({ color: 0xFFFFFF})
        var wireframe = new THREE.LineSegments(geo, mat);
        itemCube.add(wireframe)
    }
    
    addItemCube(-12, 1, -7, "Mets5 - Room 40B") //top left
    addItemCube(-12, 1, 2, "Mets5 - Room 40A") // mid left
    addItemCube(12, 1, -2, "Mets5 - Room 41A") // mid right
    addItemCube(12, 1, 7, "Mets5 - Room 41B") // bottom right

  
    renderer.domElement.addEventListener("click", function () {

        const cubeInfo = intersect[0].object.userData.room;
        
        console.log(intersect[0].object.userData)

        
        //$("#container").css("display", "block");
        $("#container").modal("show");

        $("#rackRoom").text(cubeInfo)
    });

    var jsonFile = "../json/scene.json";

    
    //const mesh = new THREE.ObjectLoader().parse(JSON.parse(str))
    
    //const state = { mesh: mesh, mixer: new THREE.AnimationMixer(mesh) }

    //const scene_json = JSON.stringify(scene.toJSON())
    //const loader = new THREE.ObjectLoader().parse( JSON.parse(scene_json) )

    //scene.add(loader)
    //loader.load(jsonFile, handle_load)

    //function handle_load( geometry, material) {
    //    var mesh = new THREE.Mesh(geometry, material)
    //    scene.add(mesh)
    //    mesh.position.x = 0
    //    mesh.position.y = 0
    //    mesh.position.z = 0
    //}
    //const on_done = (obj) => {
    //    camera.position.set(2, 2, 2);
    //    camera.lookAt(0, 0, 0);
    //    renderer.render(obj, camera);
    //};
    //loader.load(jsonFile, on_done);


    //loader.load(jsonFile, function (geometry, materials) {
    //    var mesh = new THREE.Mesh(geometry, new THREE.MeshFaceMaterial(materials))

    //});

    //loader.load(jsonFile,
    //    function (obj) {
    //        const scene_obj = loader.parse(JSON.parse(obj));
    //        scene.add(scene_obj);

    //   },

    //   function (xhr) {
    //       console.log((xhr.loaded / xhr.total * 100) + '% loaded');
    //   },

    //    function (err) {
    //        console.error(err)
    //    }
        
    //)

     /*const url = '/json/example.json';
    const on_done = (obj) => {
    camera.position.set(0,0,0);
    camera.lookAt(0,0,0);
    renderer.render(obj, camera)
    }*/
    //loader.load(url,on_done)


    //function loadGeo(json) {

    //    var load = new THREE.ObjectLoader();
    //    var obj = loader.parse(json);

    //    var pushed = false;

    //    object.traverse(function (child) {
    //        if (child.geometry !== undefined && pushed !== true) {

    //            addGeometry(child.geometry);

    //            $.each(child.material.materials, function (cm, cMat) {
    //                sD.geo.materials.push(cMat);
    //            });
    //            pushed = true
    //        }
    //    });
    //}

    function func() {
       fetch("/json/example.json")
            .then((res) => {
               return res.json();
            })
        .then((data) => console.log(data))
    }
    
    $(document).ready(function(){

        $.getJSON('../json/scene.json')
            .done(function (data) {
                //console.log('JSON Data:', data);
    })
    .fail(function(err) {
        //console.log(err);
    });

        $(".btn-cell").click(function(){
        
            var arr = $(this).closest("td").prevAll().length;
            var arrPadded = arr < 10 ? '0' + arr : arr;
            var cellInfo = "R" + arrPadded + $(this).closest("tr").text().trim();
            $("#table-header").text(cellInfo)
            $("#data-cell-title").text(cellInfo)
        })
    })
   

    $("#close-modal").click(function (e){
        $("#container").modal("hide");
    });

    $("td button").click(function (e){
        $("#data-cell-modal").modal("show");
    })

    //$("#cube-container").click(function(e){
    //    e.stopPropagation();
    //})

    $(".btn-pallet").click(function(){
        $("#pallet-modal").modal("show");
    })
 
    $("#download").click(function () {
        var serializedScene = JSON.stringify(scene.toJSON());

        var blob = new Blob([serializedScene], { type: 'application/json' })
        var url = URL.createObjectURL(blob);

        var link = document.createElement('a');
        link.href = url;
        link.download = "scene.json";
        link.click()
    })


    scene.add(jsonGroup)
    /*let save = JSON.stringify(jsonGroup)
    let sc = JSON.parse(save)*/
    

    window.addEventListener("mousemove", cubeHover);

    const raycaster = new THREE.Raycaster();
    const pointer = new THREE.Vector2();

    // Light
    function lights() {
        color = 0xFFFFFF;
        intensity = 10;
        light = new THREE.AmbientLight(color, intensity);
        //scene.add(light);
        group.add(light);
        light.name = "ambientLight";
    }

    function initSky() {

        // Add Sky
        sky = new Sky();
        sky.scale.setScalar(450000);
        //scene.add(sky);
        group.add(sky);

        sun = new THREE.Vector3();

        const effectController = {
            turbidity: 3.5,
            rayleigh: .85,
            mieCoefficient: 0,
            mieDirectionalG: .4,
            elevation: 52,
            azimuth: 180,
            exposure: renderer.toneMappingExposure
        };

        const uniforms = sky.material.uniforms;
        uniforms['turbidity'].value = effectController.turbidity;
        uniforms['rayleigh'].value = effectController.rayleigh;
        uniforms['mieCoefficient'].value = effectController.mieCoefficient;
        uniforms['mieDirectionalG'].value = effectController.mieDirectionalG;

        const phi = THREE.MathUtils.degToRad(90 - effectController.elevation);
        const theta = THREE.MathUtils.degToRad(effectController.azimuth);

        sun.setFromSphericalCoords(1, phi, theta);

        sky.name = "sky";

        uniforms['sunPosition'].value.copy(sun);

    }

    function cubeHover(event) {
        pointer.x = (event.clientX / window.innerWidth) * 2 - 1;
        pointer.y = -(event.clientY / window.innerHeight) * 2 + 1;

        raycaster.setFromCamera(pointer, camera);

        intersect = raycaster.intersectObjects(objectToIntersect);

        intersect = intersect.filter((intersect) => intersect.object.name !== "line")

        if(intersect.length > 0){
            $(canvas).css("cursor", "pointer");
        } else {
            $(canvas).css("cursor", "default");
        }
    }

    function animate(){
        renderer.render(scene, camera)
        //func()
    }
    renderer.setAnimationLoop(animate);
};

init()
