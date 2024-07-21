extends CharacterBody3D

const SPEED = 4.0

@export var player_path := "res://Scenes/Characters/Player/player.tscn"

@onready var nav_agent = $NavigationAgent3D
@onready var player = null

func _ready():
	player = find_node_in_tree(get_tree().root, "Player")
	if player == null:
		print("Player node not found")

func _process(delta):
	if player:
		velocity = Vector3.ZERO
		nav_agent.set_target_position(player.global_transform.origin)
		var next_nav_point = nav_agent.get_next_path_position()
		velocity = (next_nav_point - global_transform.origin).normalized() * SPEED
	
		nav_agent.set_velocity(velocity)
		look_at(player.global_transform.origin, Vector3.UP)

func _on_navigation_agent_3d_velocity_computed(safe_velocity):
	velocity = velocity.move_toward(safe_velocity, 0.25)
	move_and_slide()

func find_node_in_tree(node, name):
	if node.name == name:
		return node
	for child in node.get_children():
		var result = find_node_in_tree(child, name)
		if result != null:
			return result
	return null


