using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using UnityEngine.UI;
public class NewBehaviourScript : MonoBehaviour
{
    [Serializable]
    public class Qualquer
    {
        public Dictionary<string, int> dicionario = new();
        public List<string> lista = new();
        public List<ETipos> listaDeEnums = new();
        public enum ETipos { cavalo, macaco, boi }
        public List<int[]> listDeInt = new();

    }
    public Text textUi;

    // Start is called before the first frame update
    void Start()
    {
        var novoObjeto = new Qualquer();
        novoObjeto.dicionario.Add("Abacaxi", 25);
        novoObjeto.dicionario.Add("laranja", 25);
        novoObjeto.listaDeEnums.Add(Qualquer.ETipos.cavalo);
        novoObjeto.listaDeEnums.Add(Qualquer.ETipos.boi);
        novoObjeto.listDeInt.Add(new int[3] { 1, 2, 3 });
        novoObjeto.listDeInt.Add(new int[3] { 5, 1, 6 });

        var json = JsonConvert.SerializeObject(novoObjeto);
        Debug.Log(json);
        textUi.text = json;

        
     
    }

    // Update is called once per frame
    void Update()
    {

    }
}
