var App = angular.module("App", []);

App.controller("FAQController", function ($scope, $http) {

    var url = '/api/FAQ/';

    $scope.update = function() {
        $scope.brodsti = false;
        $scope.visFront = true;
        $scope.visSporsmal = false;
        $scope.visSporsmalSkjema = false;
        $scope.visAlleSporsmal = false;
        $scope.sokSpor = false;
        $('#sinfo').show();
    };

    function hentFrontKat() {
        $http.get(url).
        success(function (AlleKat) {           
            $scope.frontFAQ = AlleKat;
            $scope.laster = false;
        }).
        error(function (data, status) {
            console.log(status + data);
        });
    };

    $scope.hentSporsmalIKategori = function (id) {
        $http.get(url + id).
        success(function (Sporsmal) {
            $scope.visAlleSporsmal = false;
            $scope.Kategori = Sporsmal;
            $scope.visSporsmal = true;
            $scope.visFront = false;
            $scope.sokSpor = false;
            $scope.brodsti = true;
            var v = Sporsmal[0].kategori;
            $('#brodvalg').html(" " + v);
        }).
        error(function (data, status) {
            console.log(status + data);
        });
    };

    $scope.visAlleSporsmal = false;
    $('#sinfo').show();
    $scope.sidemeny = true;
    $scope.brodsti = false;
    $scope.visSporsmalSkjema = false;
    $scope.visFront = true;
    $scope.laster = true;
    $scope.visAlleSporsmal = false;
    $scope.sokSpor = false;

    hentFrontKat();

    $scope.visSkjema = function () {

        $scope.fornavn = "";
        $scope.epost = "";
        $scope.sporsmal = "";
        $scope.skjema.$setPristine();
        $scope.visAlleSporsmal = false;
        $scope.visSporsmalSkjema = true;
        $('#brodvalg').html(" Send inn ditt spørsmål");
        $scope.brodsti = true;
        $scope.visFront = false;
        $scope.visSporsmal = false;
        $scope.sokSpor = false;
        $('#sinfo').hide();
        
        $scope.innsending = true;
    };

    $scope.blaIgjennomSporsmal = function () {


        $http.get(url + 7).
        success(function (Sporsmal) {
            $scope.alleSporsmal = Sporsmal;

            $scope.sokSpor = false;
            $scope.visSporsmalSkjema = false;
            $scope.visSporsmal = false;
            $scope.innsending = false;       
            $scope.visFront = false;
            $scope.brodsti = true;
            $('#sinfo').show();
            $scope.visAlleSporsmal = true;
            var v = Sporsmal[0].kategori;
            $('#brodvalg').html(" " + v);
        }).
        error(function (data, status) {
            console.log(status + data);
        });
        
    };
    
    $scope.sendInnSkjema = function () {

        console.log("Inne i sendSpørsmål");

        var innFaq = {
            fornavn: $scope.fornavn,
            epostadresse: $scope.epost,
            sporsmal: $scope.sporsmal
        };
       
        $http.post(url , innFaq).
        success(function (data) {
            
            $scope.blaIgjennomSporsmal();

            console.log("Spørsmål lagret");
        }).
        error(function (data, system) {
            console.log(data + system);
        });

    };

    $scope.antallKlikk = function (antall, id) {


        var innFaq = {        
            antallKlikk: antall + 1
        };
        var target = document.getElementById(id);
        
        if (target.className == "panel-collapse collapse")
        {
            $http.put(url + id, innFaq).
            success(function () {
            
                for(var s = 0; s< $scope.alleSporsmal.length; s++)
                {
                    if($scope.alleSporsmal[s].id == id)
                    {
                       $scope.alleSporsmal[s].antallKlikk++;
                    }
                }          
            }).
            error(function (data,status) {
                console.log(data, status);
            });
        }
    };
    $scope.sokSporKnapp = function () {
        
        $scope.laster = true;
       
        $scope.visSporsmalSkjema = false;
        $scope.visSporsmal = false;
        $scope.innsending = false;
        $scope.visFront = false;
        $('#sinfo').show();
        $scope.visAlleSporsmal = false;
       
        

       
        var innhold = $scope.soko;
        var aliste = new Array();
        var j = 0;
       
        var utliste = new Array();

        $http.get(url).
        success(function (kategorier) {
          
            for (var k = 0; k < kategorier.length; k++)
            {
               
                $http.get(url + kategorier[k].id).
                success(function (spor) {
                   
                    for (var l = 0; l < spor.length; l++)
                    {
                        aliste[j++] = spor[l];
                        
                    }
                    var y = 0;
                    for (var u = 0; u < aliste.length; u++)
                    {
                        var patt = new RegExp(innhold, "i");
                        if(patt.test(aliste[u].sporsmal))
                        {
                            utliste[y++] = aliste[u];
                            
                        }
                    }
                   
                }).
                error(function (data, status) {
                    console.log(data + status);
                });
            }
            
            $('#brodvalg').html(" Søk");
            $scope.alleSpor = utliste;
            $scope.brodsti = true;

            $scope.sokSpor = true;
            $scope.laster = false;
        }).
        error(function (data, status) {
            console.log(data + status);
        });
       
    };

   
});