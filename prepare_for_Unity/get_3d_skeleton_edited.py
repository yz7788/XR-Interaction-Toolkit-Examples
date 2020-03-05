import os
import sys
import json
from tqdm import tqdm
import subprocess
import numpy as np

def triangulate(x, Ps, num_views):
  '''
    Ref: http://www.cs.cmu.edu/~16385/s17/Slides/11.4_Triangulation.pdf
    :param x: matched 2d point with size Nviews x 2
    :param Ps: camera matrix with size Nviews x 3 x 4
    :return: 3d point
  '''
  nviews = x.shape[1]
  A = np.zeros((num_views * nviews, 4))
  for i in range(nviews):
    A[2 * i, :] = x[i, 1] * Ps[i, 2, :] - Ps[i, 1, :]
    A[2 * i + 1, :] = Ps[i, 0, :] - x[i, 0] * Ps[i, 2, :]
  u, s, v = np.linalg.svd(A)
  X = v[-1, :]
  X = v[-1, :] / v[-1, -1]
  return X[:3]

def main(json_dir, output_dir, calibration_dir, cam0_json_dir, cam1_json_dir):
  json_to_process = list()
  cams = set()

  # Count cameras/views
  """
  Counting how many cam_output folders there are
  Two in our case
  """
  for case in os.listdir(json_dir):
    cams.add(case)
  num_views = len(cams)

  # Count frames
  """
  Counting how many json files we have in cam0 (the first loaded cam)
  We have to do calcuation to match cam0 and cam1 later though
  """
  for case in os.listdir(cam0_json_dir):
    if case[-5:] == '.json':
      json_to_process.append(case.strip().split('_')[0])

  # Read camera calibration (KRT-matrix)
  cams_calibration = list()
  cam_count = 0
  for cam_idx in cams:
    cam_matrix_txt = open(os.path.join(calibration_dir, 'KRT' + str(cam_count) + '.txt'), 'r')
    cam_matrix = str()
    for line in cam_matrix_txt.readlines():
      cam_matrix += line.strip() + ' '
    cam_matrix = np.array(cam_matrix.strip().split(' '), dtype=np.float32).reshape(3,4)
    cams_calibration.append(cam_matrix)
    cam_count += 1
  cams_calibration = np.array(cams_calibration)
  
  # Read and optimize 2D keypoints
  """
  Problem here is the fps difference with cam0 and cam 1
  Since cam0 loads first, it already has a few hundrend frames worth of keypoints when
  cam1 loads. We need to match cam0 and cam1's frame rate.
  Also, We need to make sure this is realtime, so the code needs to keep getting more frames
  as the two cams output more frames
  """
  for case in tqdm(json_to_process):
    pose_kpd_2d = list()
    for cam in cams:
      curr_json = json.load(open(os.path.join(json_dir, cam + '\\' + case) + '_keypoints.json', 'r'))
      pose_kpd_2d_view_json = curr_json['people'][0]['pose_keypoints_2d']
      pose_kpd_2d_view = list()
      count = 0
      for j in range(len(pose_kpd_2d_view_json) // 3):
        if count != 8 and count != 19 and count != 20 and count != 21 and count != 22 and count != 23 and count != 24:
          #don't append these keypoints b/c we are using COCO model of 18 keypoints, not 25
          pose_kpd_2d_view.append(pose_kpd_2d_view_json[j * 3: (j+1) * 3 - 1])
        count += 1
      pose_kpd_2d.append(pose_kpd_2d_view)
    pose_kpd_2d = np.array(pose_kpd_2d)
    pts3d_all = list()
    for i in range(pose_kpd_2d[0].shape[0]):
      pts2d = pose_kpd_2d[:, i]
      pts3d = triangulate(pts2d, cams_calibration, num_views)
      pts3d_all.append(pts3d)
    # Save to npy for each frame
    """ 
    Need to save to txt file here
    """
    openrig_3d_array = np.array(pts3d_all)
    openrig_3d_array = np.transpose(openrig_3d_array)

    if case == "000000000000":
      case = case.lstrip("0") + "0"
    else:
      case = case.lstrip("0")
    f = open(os.path.join(output_dir, case + '.txt'), 'w')
    f.write('[')
    f.write(str(openrig_3d_array))
    f.write(']')
    f.close()
    #f.close()
    #np.savetxt(os.path.join(output_dir, '3d_data' + case + '.txt'), openrig_3d_array, fmt='%1.8f') 
    #np.save(os.path.join(output_dir, '3d_data' + case + '.txt'), np.array(pts3d_all))

if __name__ == "__main__":
  json_dir = 'C:\\Users\\CSCI-538-HP-Z240-ll\\Downloads\\cameraOutput'
  cam0_json_dir = 'C:\\Users\\CSCI-538-HP-Z240-ll\\Downloads\\cameraOutput\\camera0output'
  cam1_json_dir = 'C:\\Users\\CSCI-538-HP-Z240-ll\\Downloads\\cameraOutput\\camera1output'
  output_dir = 'C:\\Users\\CSCI-538-HP-Z240-ll\\Downloads\\openpose-1.5.1-binaries-win64-gpu-python-flir-3d_recommended\\openpose-1.5.1-binaries-win64-gpu-python-flir-3d_recommended\\openpose\\output_3d'
  calibration_dir = 'C:\\Users\\CSCI-538-HP-Z240-ll\\Downloads\\openpose-1.5.1-binaries-win64-gpu-python-flir-3d_recommended\\openpose-1.5.1-binaries-win64-gpu-python-flir-3d_recommended\\openpose\\cameraCalibration'

  os.makedirs(output_dir, exist_ok=True)
  

  # Step 1: read in KRT for multiple cams
  # Step 2: load multi cams
  

  # Step 3: call openpose for each cams (better assigning different GPU usage)
  """
  runs batch file that opens two cmd prompts for each OpenPose using different cameras and different output folder
  currently one cam has better fps than other cam, thus creates output files faster/more than the other
  this may lead to wrong calculation
  """

  #subprocess.call([r'C:\\Users\\CSCI-538-HP-Z240-ll\Downloads\\openpose-1.5.1-binaries-win64-gpu-python-flir-3d_recommended\\openpose-1.5.1-binaries-win64-gpu-python-flir-3d_recommended\\openpose\\runTwice.bat'])

  # Step 4: format 2D skeletons
  # Step 5: call triangulation to get 3D skeleton
  # Step 6: format 3D skeleton file and save to proper location
  main(json_dir, output_dir, calibration_dir, cam0_json_dir, cam1_json_dir)