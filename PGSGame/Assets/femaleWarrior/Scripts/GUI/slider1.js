var amount : float;
private var inUse : boolean = false;
private var pixelInsetRange : Vector2 = Vector2(-0.215,100);
function Update () {
	var 	sliderPosition : Vector3;
	sliderPosition.x = transform.position.x * Screen.width + GetComponent.<GUITexture>().pixelInset.x + GetComponent.<GUITexture>().pixelInset.width * 0.5;
	sliderPosition.y = transform.position.y * Screen.height + GetComponent.<GUITexture>().pixelInset.y + GetComponent.<GUITexture>().pixelInset.height * 0.5;
	
	if(Vector3.Distance(sliderPosition, Input.mousePosition) < 12 && Input.GetMouseButtonDown(0)){
		inUse = true;
	}
	if(inUse){
		GetComponent.<GUITexture>().pixelInset.x = Input.mousePosition.x - transform.position.x * Screen.width - GetComponent.<GUITexture>().pixelInset.width * 0.5;
		amount = GetComponent.<GUITexture>().pixelInset.x / (pixelInsetRange.y - pixelInsetRange.x);
		if(Input.GetMouseButtonUp(0)){
			inUse = false;
		}
	}
	amount = Mathf.Clamp01(amount);
	GetComponent.<GUITexture>().pixelInset.x = Mathf.Lerp(-0.215,100,amount);
}

function InUse(){
	return inUse;
}
