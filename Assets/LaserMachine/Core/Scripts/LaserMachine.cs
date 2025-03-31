using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace Lightbug.LaserMachine
{


public class LaserMachine : MonoBehaviour {

    struct LaserElement 
    {
        public Transform transform;        
        public LineRenderer lineRenderer;
        public GameObject sparks;
        public bool impact;
    };

    public int laserCount = 8;

    List<LaserElement> elementsList = new List<LaserElement>();
    

    [Header("External Data")]
    
    [SerializeField] LaserData m_data;

    [Tooltip("This variable is true by default, all the inspector properties will be overridden.")]
    [SerializeField] bool m_overrideExternalProperties = true;

    [SerializeField] LaserProperties m_inspectorProperties = new LaserProperties();
    

    LaserProperties m_currentProperties;// = new LaserProperties();
        
    float m_time = 0;
        float normal_rotation;
        float slow_rotation;
    bool m_active = true;
    bool m_assignLaserMaterial;
    bool m_assignSparks;
        PlayerController controller;
  		

    void OnEnable()
    {
        m_currentProperties = m_overrideExternalProperties ? m_inspectorProperties : m_data.m_properties;
            normal_rotation = m_currentProperties.m_rotationSpeed;
            slow_rotation = normal_rotation * 0.1f;
            controller = FindAnyObjectByType<PlayerController>();




        m_currentProperties.m_initialTimingPhase = Mathf.Clamp01(m_currentProperties.m_initialTimingPhase);
        m_time = m_currentProperties.m_initialTimingPhase * m_currentProperties.m_intervalTime;
        
        float angleStep = m_currentProperties.m_angularRange / laserCount;        

        m_assignSparks = m_data.m_laserSparks != null;
        m_assignLaserMaterial = m_data.m_laserMaterial != null;

            for (int i = 0; i < laserCount; i++)
            {
                LaserElement element = new LaserElement();
                // add the Laser tag to the object

                GameObject newObj = new GameObject("lineRenderer_" + i.ToString());
                newObj.tag = "Laser";

                if( m_currentProperties.m_physicsType == LaserProperties.PhysicsType.Physics2D )
                newObj.transform.position = (Vector2)transform.position;
                else
                newObj.transform.position = transform.position;

                newObj.transform.rotation = transform.rotation;
                newObj.transform.Rotate( Vector3.up , i * angleStep );
                newObj.transform.position += newObj.transform.forward * m_currentProperties.m_minRadialDistance;

                newObj.AddComponent<LineRenderer>();
                

                if( m_assignLaserMaterial )
                newObj.GetComponent<LineRenderer>().material = m_data.m_laserMaterial;

                newObj.GetComponent<LineRenderer>().receiveShadows = false;
                newObj.GetComponent<LineRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                newObj.GetComponent<LineRenderer>().startWidth = m_currentProperties.m_rayWidth;
                newObj.GetComponent<LineRenderer>().useWorldSpace = true;
                newObj.GetComponent<LineRenderer>().SetPosition(0, newObj.transform.position);
                newObj.GetComponent<LineRenderer>().SetPosition(1, newObj.transform.position + transform.forward * m_currentProperties.m_maxRadialDistance);
                newObj.transform.SetParent(transform);

                //newObj.AddComponent<Collider>();
                // add box collider
                BoxCollider boxCollider = newObj.AddComponent<BoxCollider>();
                // set size of collider
                boxCollider.isTrigger = true;
                boxCollider.size = new Vector3(0.25f, 0.25f, m_currentProperties.m_maxRadialDistance);
                // set center of collider
                boxCollider.center = new Vector3(0, 0, m_currentProperties.m_maxRadialDistance / 2);


                
                if( m_assignSparks )
                {
                GameObject sparks = Instantiate(m_data.m_laserSparks);
                sparks.transform.SetParent(newObj.transform);
                sparks.tag = "Laser";
                // Add a small sphere collider to the sparks
                SphereCollider sparksCollider = sparks.AddComponent<SphereCollider>();
                sparksCollider.radius = 0.1f;
                sparksCollider.isTrigger = true; // Set as trigger so it doesn't affect physics
                sparks.SetActive(true);
                element.sparks = sparks;

                }

                element.transform = newObj.transform;
                element.lineRenderer = newObj.GetComponent<LineRenderer>();
                element.impact = false;

                elementsList.Add(element);
            }
        
	}
        
       
	void Update () {

        if (m_currentProperties.m_intermittent)
        {
            m_time += Time.deltaTime;

            if (m_time >= m_currentProperties.m_intervalTime)
            {
                m_active = !m_active;
                m_time = 0;
                return;
            }
        }

        RaycastHit2D hitInfo2D;
        RaycastHit hitInfo3D;

        

        foreach (LaserElement element in elementsList)
        {
            
            // Get the BoxCollider component attached to the laser element
            BoxCollider laserCollider = element.transform.GetComponent<BoxCollider>();
            
            // Check if any colliders are overlapping with the laser's box collider
            Collider[] hitColliders = Physics.OverlapBox(
                laserCollider.bounds.center, 
                laserCollider.bounds.extents, 
                element.transform.rotation, 
                m_currentProperties.m_layerMask);
            
            // Check if the player is among the hit colliders
            foreach (Collider hitCol in hitColliders)
            {
                if (hitCol.CompareTag("Player"))
                {
                    Debug.Log("Hit player");
                    // You could also add code to damage the player here
                }
            }



            if ( m_currentProperties.m_rotate )
            {
                if (controller.timeSlowed)
                    {
                        if (m_currentProperties.m_rotateClockwise)
                            element.transform.RotateAround(transform.position, transform.up, Time.deltaTime * slow_rotation);    //rotate around Global!!
                        else
                            element.transform.RotateAround(transform.position, transform.up, -Time.deltaTime * slow_rotation);
                    }
                    else
                    {
                        if (m_currentProperties.m_rotateClockwise)
                            element.transform.RotateAround(transform.position, transform.up, Time.deltaTime * normal_rotation);
                        else
                            element.transform.RotateAround(transform.position, transform.up, -Time.deltaTime * normal_rotation);
                    }

            }


            if (m_active)
            {
                element.lineRenderer.enabled = true;
                element.lineRenderer.SetPosition(0, element.transform.position);

                if(m_currentProperties.m_physicsType == LaserProperties.PhysicsType.Physics3D)
                {
                    Physics.Linecast(
                        element.transform.position,
                        element.transform.position + element.transform.forward * m_currentProperties.m_maxRadialDistance,
                        out hitInfo3D ,
                        m_currentProperties.m_layerMask
                    );  


                    if (hitInfo3D.collider)
                    {
                        Debug.Log("Hit " + hitInfo3D.collider.gameObject.name);



                        element.lineRenderer.SetPosition(1, hitInfo3D.point);

                        if( m_assignSparks )
                        {
                            element.sparks.transform.position = hitInfo3D.point; //new Vector3(rhit.point.x, rhit.point.y, transform.position.z);
                            element.sparks.transform.rotation = Quaternion.LookRotation( hitInfo3D.normal ) ;
                        }

                        /*
                        EXAMPLE : In this line you can add whatever functionality you want, 
                        for example, if the hitInfoXD.collider is not null do whatever thing you wanna do to the target object.
                        DoAction();
                        */
                        Debug.Log("Hit " + hitInfo3D.collider.gameObject.name);
                            if (hitInfo3D.collider.gameObject.tag == "Player")
                            {
                                Debug.Log("Player hit");
                                // call from player contorler script
                                // hitInfo3D.collider.gameObject.GetComponent<Player>().TakeDamage(1);
                            }



                    }
                    else
                    {
                        element.lineRenderer.SetPosition(1, element.transform.position + element.transform.forward * m_currentProperties.m_maxRadialDistance);

                    }

                    if( m_assignSparks )
                        element.sparks.SetActive( hitInfo3D.collider != null );
                }

                else
                {
                    hitInfo2D = Physics2D.Linecast( 
                        element.transform.position,
                        element.transform.position + element.transform.forward * m_currentProperties.m_maxRadialDistance,
                        m_currentProperties.m_layerMask 
                    );


                    if (hitInfo2D.collider)
                    {
                        element.lineRenderer.SetPosition(1, hitInfo2D.point);

                        if( m_assignSparks )
                        {
                            element.sparks.transform.position = hitInfo2D.point; //new Vector3(rhit.point.x, rhit.point.y, transform.position.z);
                            element.sparks.transform.rotation = Quaternion.LookRotation( hitInfo2D.normal ) ;
                        }

                        /*
                        EXAMPLE : In this line you can add whatever functionality you want, 
                        for example, if the hitInfoXD.collider is not null do whatever thing you wanna do to the target object.
                        DoAction();
                        */

                    }
                    else
                    {
                        element.lineRenderer.SetPosition(1, element.transform.position + element.transform.forward * m_currentProperties.m_maxRadialDistance);

                    }

                    if( m_assignSparks )
                        element.sparks.SetActive( hitInfo2D.collider != null );

                }              

                





            }
            else
            {
                element.lineRenderer.enabled = false;

                if( m_assignSparks )
                    element.sparks.SetActive(false);
            }
        }
        
    }

    /*
    EXAMPLE : 
    void DoAction()
    {

    }
    */

	
}


}
